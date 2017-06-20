using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Extensions;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Services
{
    public class ReminderService : KinveyServiceBase, IReminderService
    {

        #region Private Properties

        private const string EndpointTypes = "ReminderTypes";
        private const string EndpointReminders = "Reminder";
        private const string EndpointFrequency = "ReminderFrequency";

        private List<ReminderTypeModel> _reminderTypes = null;
        private List<ReminderFrequencyModel> _reminderFrequency = null;

        private readonly ICalendarService _calendarService;

        #endregion
        
        #region Public Methods

        #region CRUD
        public async Task Delete(string petId, ReminderModel reminder)
        {
            try
            {
                var response = await DeleteAppdataAsync<KReminder>(EndpointReminders, reminder.Id);
                _calendarService.RemoveReminder(reminder);
                RemoveReminderFromPet(petId, reminder.Id);
                return;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                return;
            }
        }

        public async Task Save(ReminderModel reminder, string petId)
        {
            KReminder kReminder = reminder.GenerateKReminder(petId);
            try
            {
                var json = JsonConvert.SerializeObject(kReminder);
                var response = await SaveAppdataAsync<KReminder>(EndpointReminders, kReminder);
                if (string.IsNullOrEmpty(response.Id))
                    throw new Exception($"{response.ErrorCode} : {response.ErrorDetail}");

                kReminder.Id = response.Id;
                AddReminderToPet(kReminder);
                return;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        //public async Task<List<ReminderModel>> Get(string petId)
        //{
        //    var kpet = Settings.CurrentUserProfile?.Pets?.FirstOrDefault(pet => pet.Id == petId);
        //    return kpet?.Reminders?.Select(r=>ReminderModel.CreateFrom(r, kpet.ExpandedImages?.KMedium?.DownloadURL, kpet.Name)).ToList();
        //}

        public async Task<ReminderModel> GetById(string reminderId)
        {
            var kreminderList = await this.GetAllReminder();

            var kReminder = kreminderList.SingleOrDefault(km => km.Id == reminderId);

            if (kReminder == null) return null;

            return ReminderModel.CreateFrom(kReminder, string.Empty, string.Empty);
        }

        #endregion

        public async  Task<List<ReminderTypeModel>> GetAllReminderType()
        {
            if (_reminderTypes != null && _reminderTypes.Any()) return _reminderTypes;

            var reminderTypes = await Execute<List<KReminderType>>($"getReminderTypes", () => GetAppDataAsync<KReminderType>(EndpointTypes), LocalCachingPolicy.NoPersistence, RequestErrorHandlingPolicy.ThrowErrorOnStack);


            var result = reminderTypes.Select(ReminderTypeModel.CreateFrom).ToList();
            result.RemoveAll(item => item == null);
            result.Add(new ReminderTypeModel
            {
                Name = "Other",
                Type = ReminderType.Other,
                SubType = ReminderSubType.Other
            });
            _reminderTypes = result;

            return _reminderTypes;
        }

        public async Task<List<ProductModel>> GetProducts(ReminderType type)
        {

            var types = await  GetAllReminderType();

            var typeReminder = types.FirstOrDefault(t => t.Type == type);

            var products = typeReminder.Products;
            if (products.FindIndex(p => p.IsOtherType) == -1)
            {
                products.Add(new ProductModel
                {
                    Name = "Other",
                    Type = ReminderSubType.Other
                });
            }
            

            return products;
        }

        public async Task<List<ReminderFrequencyModel>> GetRecurrence()
        {
            if (_reminderFrequency != null && _reminderFrequency.Any()) return _reminderFrequency;

            var reminderFrequency = await Execute<List<KFrequency>>($"getReminderFrequency", () => GetAppDataAsync<KFrequency>(EndpointFrequency, null), LocalCachingPolicy.NoPersistence, RequestErrorHandlingPolicy.ThrowErrorOnStack);
            
            var result = reminderFrequency.Select(ReminderFrequencyModel.CreateFrom).ToList();
            result.RemoveAll(item => item == null);

            _reminderFrequency = result;

            return _reminderFrequency;
        }
        
        public async Task<List<KReminder>> GetAllReminder(string profileId = null)
        {
            profileId = profileId ?? Settings.CurrentUserProfile.Id;
            var query = String.Format(@"{{""profile_id"":""{0}""}}", profileId);
            return await GetAppDataAsync<KReminder>(EndpointReminders, query);
        }

        #endregion

        #region Private Methods

        private void RemoveReminderFromPet(string petId, string id)
        {

            var profile = Settings.CurrentUserProfile;
            profile.Pets.FirstOrDefault(p => p.Id == petId)?.Reminders?.RemoveAll(r => r.Id == id);
            Settings.CurrentUserProfile = profile;
        }

        private void AddReminderToPet(KReminder kReminder)
        {
            var profile = Settings.CurrentUserProfile;
            var kpet =
                profile.Pets.FirstOrDefault(pet => pet.Id == kReminder.PetId);

            var oldReminder = kpet.Reminders?.Find(r => r.Id == kReminder.Id);

            if (kpet.Reminders != null && oldReminder != null)
            {
                kpet.Reminders[kpet.Reminders.IndexOf(oldReminder)] = kReminder;
            }
            else
            {
                if (kpet.Reminders == null)
                    kpet.Reminders = new List<KReminder>();
                kpet.Reminders.Add(kReminder);
            }
            Settings.CurrentUserProfile = profile;
        }

        #endregion

        #region Constructeurs
        public ReminderService(IKinveyService kinveyService, ICalendarService calendarService) : base(kinveyService)
        {
            _calendarService = calendarService;
            InitService();
        }

        #endregion

        #region Private Methods
        private void InitService()
        {
          //dsmvx   GetAllReminderType();
        //dsmvx     GetRecurrence();
        }

        public Task<List<ReminderModel>> Get(string petId)
        {
            return null; //dsmvx  throw new NotImplementedException();
        }

        #endregion
    }
}
