using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Extensions;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;
using MvvmValidation;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
          



    public class PetReminderProductDetailViewModel : PetReminderBaseDetailViewModel
    {

        #region Fields
         
        private List<ReminderTypeModel> _reminderTypes;
        private readonly IReminderService _reminderService;
        private PetReminderModel _petReminderModel;

        #endregion

        public PetReminderProductDetailViewModel(
            IReminderService reminderService,
            ICalendarService calendarService, 
            IPopupService popupService) : base(reminderService, calendarService, popupService)
        {
            _reminderService = reminderService;
              LoadPetReminderTypes();
        }

        public async override Task CompleteReminderModel()
        {
            await base.CompleteReminderModel();
            Reminder.ProductModel = Reminder.TypeModel.Products.FirstOrDefault(p=>p.Id == Reminder.ProductModel.Id)?? Reminder.ProductModel;
            Reminder.SetDefaultFrequency();
            RaisePropertyChanged(() => Reminder);

        }

                private async Task<bool> LoadPetReminderTypes()
        {
            _reminderTypes = await _reminderService.GetAllReminderType();
            return true;
        }

        public override void AddValidationFields(ValidationHelper helper)
        {
            base.AddValidationFields(helper);

            //helper.AddRule(() => Reminder.FirstAlert,
            //    () => RuleResult.Assert(Reminder.FirstAlert >= DateTime.UtcNow, "The first alert must be in futur"));
        }
    }
}