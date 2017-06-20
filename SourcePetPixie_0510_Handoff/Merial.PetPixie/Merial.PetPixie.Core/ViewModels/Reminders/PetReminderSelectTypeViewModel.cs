using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
    public class PetReminderSelectTypeViewModel: PetReminderSelectBaseViewModel<PetReminderSelectTypeParameter>
    {
        #region Fields
        
        private List<ReminderTypeModel> _reminderTypes;
        private ReminderTypeModel _selectedItem;
        private PetReminderModel _petReminderModel;
        private PetReminderSelectTypeParameter _parameter;

        #endregion

        #region Constructors

        public PetReminderSelectTypeViewModel(IReminderService reminderService) : base(reminderService)
        {
        }

        #endregion

        #region Public Properties

        public ReminderTypeModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged(() => SelectedItem);
                    ItemSelectChange();
                }
            }
        }

        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                this.SetProperty(ref this._petReminderModel, value);
                RaisePropertyChanged(() => PetReminderModel);
            }
        }
        
        public List<ReminderTypeModel> ReminderTypes { get; set; }

        #endregion
        
        #region Commands
      
        protected override void NextCommandCore()
        {
            base.NextCommandCore();
            if (SelectedItem==null)
            {
                ErrorMessage = "You have to specify a type of reminder.";
                return;
            }

            switch (SelectedItem.SubType)
            {
                case ReminderSubType.Product:
                    CreateProductionReminder();
                    break;
                case ReminderSubType.Other:
                    CreateOtherReminder();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateOtherReminder()
        {
            var reminder = ReminderModel.CreateFrom(SelectedItem, _parameter.PetReminderModel);
            ShowValidationViewModel<PetReminderOtherDetailViewModel, PetReminderBaseDetailParameter>(
                new PetReminderBaseDetailParameter
                {
                    Reminder = reminder,
                    PetReminderModel = _parameter.PetReminderModel
                });
        }

        private void CreateProductionReminder()
        {
            var reminder = ReminderModel.CreateFrom(SelectedItem, _parameter.PetReminderModel);
            ShowViewModel<PetReminderSelectProductViewModel, PetReminderSelectProductParameter>(new PetReminderSelectProductParameter
            {
                Reminder = reminder
            });
        //    Close(this);
        }
        
        #endregion

        #region Private Methods

        private void ItemSelectChange()
        {
            ErrorMessage = "";
            ShowInputOtherValue = SelectedItem.Type == ReminderType.Other;
        }

        #endregion

        #region Init

        protected override void RealInit(PetReminderSelectTypeParameter parameter)
        {
            ReminderTypes = parameter.ReminderTypes;
            this.PetReminderModel = parameter.PetReminderModel;
            _parameter = parameter;
            if (ReminderTypes != null && ReminderTypes.Any())
            {
                Items = new ObservableCollection<IItemRadio>(ReminderTypes);
            }
        }
        
        #endregion
    }

    public class PetReminderSelectTypeParameter : PetReminderListParameter
    {
        public List<ReminderTypeModel> ReminderTypes;
    }
}
