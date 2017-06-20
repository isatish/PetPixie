using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace Merial.PetPixie.Core.Services {
	public class PetService : KinveyServiceBase, IPetService
    {
		private const string Endpoint = "Pet";
	    private readonly ICalendarService _calendarService;
        public PetService(IKinveyService kinveyService, ICalendarService calendarService) : base(kinveyService)
        {
            _calendarService = calendarService;
        }

	    public async Task<IEnumerable<KPetWithReminders>> GetAllAsync(KProfile profile, bool withDetails = false)
        {
			try
            {
			    if (withDetails)
                {
                    var petIds = new StringBuilder();
                    
                    for (int index = 0; index < profile.Pets.Length; index++)
                    {
                        petIds.Append('"');
                        var kPet = profile.Pets[index];
                        petIds.Append(kPet.Id);
                        petIds.Append('"');
                        if (index != profile.Pets.Length-1)
                        {
                            petIds.Append(',');
                        }
                    }
                    var query = String.Format(@"{{""_id"":{{""$in"":[{0}]}}}}", petIds.ToString());
                    return  await GetAppDataAsync<KPetWithReminders>("Pet", query);
                }
                else
                {
                    return profile.Pets;
                }
               
            }
			catch (Exception e) {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
			}
			return new List<KPetWithReminders>();
		}

                public async Task<bool> Save(PetModel pet)
                {
                        KPetWithReminders kpet = pet.GenerateKPet();

                        if (kpet.OwnerProfileId == null)
                        {
                                kpet.OwnerProfileId = Settings.CurrentUserProfile.Id;
                        }
                        try
                        {
                                var petRetour = await SaveAppdataAsync<KPetBase>("Pet", kpet);
                                KPetWithReminders petWithReminder = KPetWithReminders.CreateFrom(petRetour);
                                petWithReminder.Reminders = kpet.Reminders;
                                petWithReminder.Breeds_ids = petWithReminder.Breed_ids;

                                AddPetToProfile(petWithReminder);
                                return true;
                        }
                        catch (Exception e)
                        {
                                _logger.Error(e);
                                Debug.WriteLine(e.Message);
                                throw;
                        }
                }

	    public async Task<bool> RemovePet(PetModel pet)
	    {
            try
            {
                //TODO Remove reminder and Event in calendar
                var response =await DeleteAppdataAsync<KPetWithReminders>("Pet", pet.Id);
                Settings.CurrentUserProfile= RemovePetFromProfile(pet.Id, Settings.CurrentUserProfile);

                foreach (var reminderModel in pet.Reminders)
                {
                    _calendarService.RemoveReminder(reminderModel);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void AddPetToProfile(KPetWithReminders pet)
        {
            var profile = Settings.CurrentUserProfile;
            if (profile.PetIds == null)
                profile.PetIds = new string[1];

            RemovePetFromProfile(pet.Id, profile);
            List<string> ids = new List<string>(profile.PetIds);
            ids.Add(pet.Id);
            profile.PetIds = ids.ToArray();
            List<KPetWithReminders> pets = new List<KPetWithReminders>(profile.Pets);
            pets.Add(pet);
            profile.Pets = pets.ToArray();

            Settings.CurrentUserProfile = profile;
        }

        private KProfile RemovePetFromProfile(string idPet, KProfile  profile)
        {
            if (profile.PetIds.Contains(idPet))
            {
                profile.PetIds = profile.PetIds.Where(id => id != idPet).ToArray();
                profile.Pets = profile.Pets.Where(p => p.Id != idPet).ToArray();
            }
            return profile;
        }
    }
}