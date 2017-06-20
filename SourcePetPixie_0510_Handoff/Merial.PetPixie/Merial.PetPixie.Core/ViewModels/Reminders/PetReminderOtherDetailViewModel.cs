using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
    public class PetReminderOtherDetailViewModel : PetReminderBaseDetailViewModel
    {
        #region Fields

        private bool _showProduct;
        private List<ReminderFrequencyModel> _recurrencesType;
        private string _petImageUrl = "";
        private string _petName = "";
        private List<ReminderTypeModel> _reminderTypes;
        private readonly IReminderService _reminderService;
        private PetReminderModel _petReminderModel;

        #endregion

        #region Constructeur

        public PetReminderOtherDetailViewModel(
            IReminderService reminderService,
            ICalendarService calendarService,
            IPopupService popupService) : base(reminderService, calendarService, popupService)
        {
            _reminderService = reminderService;
            LoadPetReminderTypes();
        }

        #endregion

        #region Public  properties



        public bool ShowProduct
        {
            get { return _showProduct; }
            set
            {
                _showProduct = value;
                RaisePropertyChanged(() => ShowProduct);
            }
        }

        public List<ReminderFrequencyModel> RecurrencesType
        {
            get { return _recurrencesType; }
            set
            {
                _recurrencesType = value;
                RaisePropertyChanged(() => RecurrencesType);
            }
        }

        #endregion

        #region Methods Override

        protected override void RealInit(PetReminderBaseDetailParameter parameter)
        {
            base.RealInit(parameter);
            ShowProduct = Reminder.ProductModel != null;
            _petReminderModel = Reminder.PetReminderModel;
          
        }

        protected async override Task<bool> LoadDataAsync()
        {
            RecurrencesType = await ReminderService.GetRecurrence();

            return await base.LoadDataAsync();
        }

        public override Task CompleteReminderModel()
        {
            Reminder.FrequencyModel =
               RecurrencesType.FirstOrDefault(f => f.IntervalValid == Reminder.FrequencyModel.IntervalValid) ??
               Reminder.FrequencyModel;

            if (Reminder.VetReminderModel != null)
            {
                var vets = Settings.CurrentUserProfile.Vets.Select(VetReminderModel.CreateFrom);
                Reminder.VetReminderModel =
                    vets.FirstOrDefault(f => f.Id == Reminder.VetReminderModel.Id) ??
                    Reminder.VetReminderModel;
            }

            return base.CompleteReminderModel();
        }

                
        private async Task<bool> LoadPetReminderTypes()
        {
            _reminderTypes = await _reminderService.GetAllReminderType();
            return true;
        }


        public override void AddValidationFields(ValidationHelper helper)
        {
            base.AddValidationFields(helper);
            if (!ShowProduct)
            {
                helper.AddRule(() => Reminder.VetReminderModel,
                    () => RuleResult.Assert(Reminder.VetReminderModel != null, "The Location must be fill."));
            }
            else
            {
                helper.AddRule(() => Reminder.ProductModel.OtherValue,
                    () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Reminder.ProductModel.OtherValue), "The product must be fill"));
            }
            if (Reminder.IsOtherType)
            {
                helper.AddRule(() => Reminder.TypeModel.OtherValue,
                    () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Reminder.TypeModel.OtherValue), "The reminder type must be fill"));
            }

            //helper.AddRule(() => Reminder.FirstAlert,
            //    () => RuleResult.Assert(Reminder.FirstAlert >= DateTime.UtcNow, "The first alert must be in futur"));

            helper.AddRule(() => Reminder.FrequencyModel,
               () => RuleResult.Assert(Reminder.FrequencyModel != null, "Please add a recurrence"));
        }

        #endregion




        #region Commands

        public ICommand SelectReminderTypeCommand => new SafeMvxCommand(() =>
        {

            ShowViewModel<PetReminderSelectTypeViewModel, PetReminderSelectTypeParameter>(
            new PetReminderSelectTypeParameter
            {
                ReminderTypes = _reminderTypes,
                PetReminderModel = _petReminderModel
            });

            
        });

        #endregion
    }
}