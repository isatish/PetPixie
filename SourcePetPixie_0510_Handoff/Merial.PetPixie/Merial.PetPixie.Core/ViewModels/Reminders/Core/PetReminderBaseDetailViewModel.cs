using System;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels.Reminders.Core 
{
    public class PetReminderBaseDetailViewModel : ValidationFormViewModelBase<PetReminderBaseDetailParameter>
    {
        #region Fields

        private ReminderModel _reminder;
        public IReminderService ReminderService { get; }

        private readonly IPopupService _popupService;
        private readonly ICalendarService _calendarService;
        private PetReminderBaseDetailParameter _parameter;
        private PetReminderModel _petReminderModel;
        private string _picturePetUrl;
        private string _petName;
        private EntityMode _entityMode;

        #endregion

        #region Constructors

        public PetReminderBaseDetailViewModel(IReminderService reminderService, ICalendarService calendarService, IPopupService popupService)
        {
            ReminderService = reminderService;
            _calendarService = calendarService;
            _popupService = popupService;
        }

        #endregion

        #region Public Properties

        public ReminderModel Reminder
        {
            get { return _reminder; }
            set
            {
                _reminder = value;
                RaisePropertyChanged(() => Reminder);
            }
        }

        public string PicturePetUrl
        {
            get { return _picturePetUrl; }
            set
            {
                _picturePetUrl = value;
                RaisePropertyChanged(() => PicturePetUrl);
            }
        }

                
        public string PetName
        {
            get { return _petName; }
            set
            {
                _petName = value;
                RaisePropertyChanged(() => PetName);
            }
        }


        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                if (this._petReminderModel == value) return;
                this.SetProperty(ref this._petReminderModel, value);
            }
        }

        public EntityMode EntityMode
        {
            get { return _entityMode; }
            set
            {
                if (this._entityMode == value) return;
                this.SetProperty(ref this._entityMode, value);
            }
        }

        #endregion

        #region Protected Methods

        protected override void RealInit(PetReminderBaseDetailParameter parameter)
        {
            Reminder = parameter.Reminder;
            _parameter = parameter;
            PicturePetUrl = parameter.Reminder.PetReminderModel.PetPictureUrl;
            PetName = parameter.Reminder.PetReminderModel.PetName;
            EntityMode = parameter.EntityMode;
        }

        protected async override Task<bool> LoadDataAsync()
        {
            if (Reminder.NeedToCompleted)
            {
                await CompleteReminderModel();
                Reminder.NeedToCompleted = false;
            }
            return await base.LoadDataAsync();
        }

        #endregion

        public virtual async Task CompleteReminderModel()
        {
            var types = await ReminderService.GetAllReminderType();
            if (Reminder.Type != ReminderType.Other)
                Reminder.TypeModel = types.FirstOrDefault(t => t.Id == Reminder.TypeModel.Id) ?? Reminder.TypeModel;
        }

        #region Commandes

                #region Commands

            public IMvxCommand SelectReminderTypeCommand => new SafeMvxCommand(() =>
    {
        ShowViewModel<PetReminderSelectTypeViewModel>();

    });





        #endregion

        public IMvxCommand ValidateCommand => new SafeMvxCommandAsync(ValidateCore);

        private async Task ValidateCore()
        {
            if (!Validate())
                return;

            IsFetchingData = true;

            SaveEventInCalendar();

            try
            {
                await ReminderService.Save(Reminder, _parameter.PetReminderModel.PetId);
                
            }
            catch (Exception e)
            {
                IsFetchingData = false;
                Logger.Error(e);
                await PopupService.DisplayMessageAsync("Error","An unexpected error occured. Please try again");
                return;
            }
            IsFetchingData = false;

            ShowViewModel<PetReminderListViewModel, PetReminderListParameter>(new PetReminderListParameter
            {
                PetReminderModel = _parameter.PetReminderModel
            });

            ShowToastToConfirmCreation();
        }

        private void ShowToastToConfirmCreation()
        {
            if (Reminder.FrequencyModel != null)
            {
                var adIteration = Reminder.FirstAlert < DateTime.Now ? 1 : 0;

                string message = "You will receive a reminder ";
                switch (Reminder.FrequencyModel.Type)
                {
                    case ReminderFrequencyType.Daily:
                        message += $"from {Reminder.FirstAlert.AddDays(adIteration).ToString("M")} {Reminder.FrequencyModel?.Name}";
                        break;
                    case ReminderFrequencyType.Weekly:
                        message += $"on {Reminder.FirstAlert.AddDays(adIteration*7).ToString("dddd")} {Reminder.FrequencyModel?.Name}";
                        break;
                    case ReminderFrequencyType.Monthly:
                        message += $"on {Reminder.FirstAlert.AddMonths(adIteration).ToString("M")} {Reminder.FrequencyModel?.Name}";
                        break;
                    case ReminderFrequencyType.Yearly:
                        message += $"on {Reminder.FirstAlert.AddYears(adIteration).ToString("M")} {Reminder.FrequencyModel?.Name}";
                        break;
                    case ReminderFrequencyType.Never:
                        if (adIteration > 0)
                            return;
                        message += $"the {Reminder.FirstAlert.ToString("D")}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _popupService.DisplayMessageAsToast(message);
            }
        }

        private void SaveEventInCalendar()
        {
            if (Reminder.SaveInCalendar)
            {
                _calendarService.AddReminder(Reminder);
            }
            else if (Reminder.IdCalendarAndroid > 0)
            {
                _calendarService.RemoveReminder(Reminder);
            }
        }


        public IMvxCommand DeleteReminderCommand => new SafeMvxCommandAsync(DeleteReminderCode);

        private async Task DeleteReminderCode()
        {
            if ( await PopupService
               .DisplayYesNoMessage("Remove reminder ?", $"Are you sure you want to remove this reminder",
                   "Yes", "No"))
            {
                IsFetchingData = true;
                await ReminderService.Delete(this.Reminder.PetReminderModel.PetId, Reminder);
                
                IsFetchingData = false;
                Close(this);
            }
        }

        #endregion
    }

    public class PetReminderBaseDetailParameter : PetReminderListParameter
    {
        public ReminderModel Reminder;
        public ViewModelBase Parent;

        public EntityMode EntityMode { get; set; } = EntityMode.New;
    }
}
