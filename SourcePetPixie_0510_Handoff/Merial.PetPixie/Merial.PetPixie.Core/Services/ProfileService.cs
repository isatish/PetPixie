using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Newtonsoft.Json.Linq;

namespace Merial.PetPixie.Core.Services
{
	public class ProfileService : KinveyServiceBase, IProfileService
    {
	    private readonly IPopupService _popupService;
        private readonly IPetService _petService;
	    private readonly IReminderService _reminderService;

        private const int LimitProfileItem = 20;

        public event EventHandler<ProfilChangeArgs> OnProfileChangeEvent;
        public ProfileService(IKinveyService kinveyService, IPopupService popupService, IPetService petService, IReminderService reminderService) : base(kinveyService)
	    {
	        _popupService = popupService;
	        _petService = petService;
            _reminderService = reminderService;
	    }

	    public async Task<KProfile> GetProfileById(string profileId, bool isAuthentification =false) {
			try {
				//It's for profile current User 
				if (Settings.CurrentUserProfile!= null)
				{
					if (isAuthentification || profileId == Settings.CurrentUserProfile.Id)
					{
						return await ReloadProfile(profileId);
					}
				}
                var result = await base.GetAppDataEntityAsync<KProfile>("Profile", profileId);
                return result;
            }
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw e;
            }
        }

		public async Task<KProfile> EditProfile(KProfile editedProfile, bool forceSaveLocal = false) {
			try
			{
                if(forceSaveLocal)
                    Settings.CurrentUserProfile = editedProfile;
                editedProfile.FullName = (editedProfile.FullName + "").Trim();
                var result = await base.SaveAppdataAsync("Profile", editedProfile);
	            Settings.CurrentUserProfile = result;
                OnProfileChangeEvent?.Invoke(this,new ProfilChangeArgs(result));
                return result;
            }
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw e;
            }
        }

		public async Task DeleteAccountAsync() {
            await DeleteAppdataAsync<KPetWithReminders>("Profile", Settings.CurrentUserProfile.Id);
        }

		public async Task<KInappropriateReport> ReportUser(string userId) {
			try {
				var res = await SaveAppdataAsync("InappropriateReport", new KInappropriateReport {
					ProfileId = userId
				});
				_popupService.DisplayMessageAsToast("Thanks for your reporting");
				return res;
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<KUserRelationship> BlockUser(string shownProfileId) {
			try {
                
                return await SaveAppdataAsync("UserRelationship", new KUserRelationship {
					FromProfileId = Settings.CurrentUserProfile.Id,
					ToProfileId = shownProfileId,
					Type = KUserRelationship.TYPE_BLOCK
				});
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<bool> IsUserBlocked(string userId) {
			try {
				return (await GetAppDataAsync<KUserRelationship>("UserRelationship",
					new {
						to_profile_id = userId,
						from_profile_id = Settings.CurrentUserProfile.Id,
						type = KUserRelationship.TYPE_BLOCK
					})).Any();
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<bool> UnblockUser(string userId) {
			try {
				var relationships = await GetAppDataAsync<KUserRelationship>("UserRelationship",
					new {
						to_profile_id = userId,
						from_profile_id = Settings.CurrentUserProfile.Id,
						type = KUserRelationship.TYPE_BLOCK
					});
				return await DeleteAppdataAsync<KUserRelationship>("UserRelationship", relationships.FirstOrDefault()?.Id);
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
		}
        
	    public async  Task<KProfile> ReloadProfile(string profileId =null)
	    {
	        profileId = profileId ?? Settings.CurrentUserProfile.Id;
            try
            {
                var result = await base.GetAppDataEntityAsync<KProfile>("Profile", profileId);
                var reminders = await _reminderService.GetAllReminder(profileId);
                var pets= await _petService.GetAllAsync(result,true);
                AddReminderToPet(reminders,  ref  pets);
                result.Pets = pets.ToArray();
                
                Settings.CurrentUserProfile = result;
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
	        return Settings.CurrentUserProfile;
	    }

	    private  void AddReminderToPet(List<KReminder> reminders, ref IEnumerable<KPetWithReminders> pets)
	    {
	        foreach (var reminder in reminders)
	        {
	            var pet = pets.FirstOrDefault(p => p.Id == reminder.PetId);
	            if (pet != null)
	            {
	                if (pet.Reminders == null)
	                    pet.Reminders = new List<KReminder>();
	                pet.Reminders.Add(reminder);
	            }
	        }
	    }

	    public async Task<List<KProfile>> GetProfileBySearchText(string searchText)
        {

			if (searchText.StartsWith("@", StringComparison.CurrentCulture))
				searchText = searchText.Substring(1);

            var regex = String.Format(@"{{""$or"":[{{""searchable_display_username"":{{""$regex"":""^.*?{0}""}}}},{{""searchable_full_name"":{{""$regex"":""^.*?{0}""}}}}]}}", searchText);
            //var regex = String.Format(@"{{""searchable_display_username"":{{""$regex"":""^.*?{0}""}}}}", searchText);
            var profiles = await GetAppDataAsync<KProfile>("Profile",regex);
            return profiles;
        }

		public async Task<KVetRating> RateVet(string vetId, int rate) {
			try {
				return await SaveAppdataAsync("VetRating", new KVetRating {
					ProfileId = Settings.CurrentUserProfile.Id,
					UserId = Settings.CurrentUser.Id,
					VetId = vetId,
					Rate = rate
				});
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
		}

		public async Task RemoveRateVet(string vetId) {
			try {
				var vetRating = await GetAppDataAsync<KVetRating>("VetRating",
					new {
						profile_id = Settings.CurrentUserProfile.Id,
						user_id = Settings.CurrentUser.Id,
						vet_id = vetId
					});
				var tasks = new List<Task>();
				foreach (var kVetRating in vetRating) {
					tasks.Add(DeleteAppdataAsync<KVetRating>("VetRating", kVetRating.Id));
				}
				await Task.WhenAll(tasks.ToArray());
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
				throw;
			}
        }

		public Task<int> GetPostCountByPet(string petId, string profileId)
		{
		    var query = $"{{\"$and\":[{{\"profile_id\":\"{profileId}\"}},{{\"pets_ids\":{{\"$in\":[\"{petId}\"]}}}}]}}";
            var taskCompletionSource = new TaskCompletionSource<int>();
			Task.Run(() => {
				try {
                    var asyncAppData = KinveyService.GetClient().AppData<JObject>("Media", typeof(JObject));
				    asyncAppData.GetCount(query, new KinveyDelegate<uint>
                    {
                        onSuccess = (response) =>
                        {
                            taskCompletionSource.SetResult((int)response);
                        },
                        onError = (error) =>
                        {
                            taskCompletionSource.SetException(error);
                        }
                    });
				}
				catch (Exception e)
                {
                    _logger.Error(e);
                    taskCompletionSource.SetException(e);
				}
			});
			return taskCompletionSource.Task;
		}

	    
    }

    public class ProfilChangeArgs : EventArgs
    {
        public KProfile Profile { get; set; }

        public ProfilChangeArgs(KProfile profile)
        {
            Profile = profile;
        }
    }
}
