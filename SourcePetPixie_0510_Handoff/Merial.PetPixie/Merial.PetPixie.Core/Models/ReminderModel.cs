using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class ReminderModel : EntityBase
    {
        #region Fields

        private ReminderType _type;
        private ReminderTypeModel _typeModel;
        private ProductModel _productModel;
        private PetReminderModel _petReminderModel;
        private string _note;
        private bool _saveInCalendar;
        private DateTime _firstAlert;
        private ReminderFrequencyModel _frequencyModel;
        private VetReminderModel _vetReminderModel;
        private string _name;

        #endregion

        #region Constructors

        public ReminderModel()
        {
            
        }

        #endregion

        #region Public Properties

       

        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                if (this._petReminderModel == value) return;
                this.SetAndNotifyPropertyChanged(ref this._petReminderModel, value);
            }
        }
        

     

        public ReminderType Type
        {
            get
            {
                return _type;
            }

            set
            {
                this.SetAndNotifyPropertyChanged(ref _type, value);
            }
        }

        public ReminderTypeModel TypeModel
        {
            get { return _typeModel; }
            set { this.SetAndNotifyPropertyChanged(ref _typeModel, value); }
        }

        public ProductModel ProductModel
        {
            get { return _productModel; }
            set { this.SetAndNotifyPropertyChanged(ref _productModel, value); }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                
                this.SetAndNotifyPropertyChanged(ref _note, value);
            }
        }

        public bool SaveInCalendar
        {
            get { return _saveInCalendar; }
            set
            {
                this.SetAndNotifyPropertyChanged(ref _saveInCalendar, value);
            }
        }

        public DateTime FirstAlert
        {
            get { return _firstAlert; }
            set
            {
                this.SetAndNotifyPropertyChanged(ref _firstAlert, value);
                this.OnPropertyChanged(NextAlertDisplay);
            }
        }

        public ReminderFrequencyModel FrequencyModel
        {
            get { return _frequencyModel; }
            set { this.SetAndNotifyPropertyChanged(ref _frequencyModel, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.SetAndNotifyPropertyChanged(ref _name, value); }
        }

        public long IdCalendarAndroid { get; set; } 
        public string NextAlertDisplay => FirstAlert.ToString("ddd MMMM dd yyyy");

        public string Id { get; set; }


        public VetReminderModel VetReminderModel
        {
            get { return _vetReminderModel; }
            set { this.SetAndNotifyPropertyChanged(ref _vetReminderModel, value); }
        }

        public bool IsOtherType => TypeModel.Type == ReminderType.Other;


        public bool NeedToCompleted { get; set; }

        #endregion

        #region Static Methods

        public static ReminderModel CreateFrom(KReminder kReminder,string petPictureUrl, string petName)
        {
            long eventId = 0;
            long.TryParse(kReminder.EventId, out eventId);
            var typeModel =
                ReminderTypeModel.CreateFrom(
                    (ReminderType)
                        int.Parse(string.IsNullOrEmpty(kReminder.ReminderTypeId) ? "0" : kReminder.ReminderTypeId),
                    kReminder.TypeName);
            return new ReminderModel
            {
                Type =(ReminderType)int.Parse(string.IsNullOrEmpty(kReminder.ReminderTypeId)?"0": kReminder.ReminderTypeId),
                FirstAlert = kReminder.ReminderDate ?? DateTime.Now,
                Id = kReminder.Id,
                //IdCalendarAndroid =long.Parse(kReminder.EventId??-1),
                Name = typeModel.NameDisplay,
                ProductModel = ProductModel.CreateFrom(kReminder.ProductId, kReminder.OtherProductName),
               
                TypeModel = typeModel,
                FrequencyModel = ReminderFrequencyModel.CreateFrom(kReminder.ReminderOccurenceIntervalId??0),
                VetReminderModel = VetReminderModel.CreateFrom(kReminder.LocationVetId,kReminder.LocationCustomText),
                SaveInCalendar = kReminder.SyncCalendar,
                Note = kReminder.Notes,
                NeedToCompleted= true,
                IdCalendarAndroid = eventId,
                PetReminderModel = new PetReminderModel
                {
                    PetId = kReminder.PetId,
                    PetPictureUrl = petPictureUrl,
                    PetName = petName,

                    
                }
                

            };
        }

        public static ReminderModel CreateFrom(ReminderTypeModel reminderTypeModel, PetReminderModel petModel)
        {
            return new ReminderModel
            {
                Type = reminderTypeModel.Type,
                TypeModel = reminderTypeModel,
                PetReminderModel = petModel,
				FirstAlert = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day,7,0,0),
            };
        }

      

        #endregion

        public string  GenerateTitleEvent()
        {
            var productName = ProductModel?.Type == ReminderSubType.Other ? ProductModel.OtherValue : ProductModel?.Name;

            switch (TypeModel.Type)
            {
                case ReminderType.Other:
                    return $"{PetReminderModel.PetName}, {TypeModel.OtherValue}";
                case ReminderType.VetVisit:
                    return $"{PetReminderModel.PetName} Vet Visit";
                default:
                    return $"{PetReminderModel.PetName}, product : {productName}";
            }
        }

        public string GenerateContentEvent()
        {
            return TypeModel.Type == ReminderType.VetVisit ? $"Adresse of the vet :{VetReminderModel.Adresse}, phone number : {VetReminderModel.PhoneNumber}." : null;
        }
    }
}
