using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
    public class PetReminderSelectProductViewModel : PetReminderSelectBaseViewModel<PetReminderSelectProductParameter>
    {
        #region Fields

        private ReminderModel _reminder;
        private ProductModel _selectedItem;
        private PetReminderModel _petReminderModel;
        private PetReminderSelectProductParameter _parameter;

        #endregion

        #region Constructeurs

        public PetReminderSelectProductViewModel(IReminderService reminderService) : base(reminderService)
        {
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

        public ProductModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                ItemSelectChange();
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

        #endregion

        #region Private Methods

        private void ItemSelectChange()
        {
            ErrorMessage = "";
            ShowInputOtherValue = false;
        }
        
        #endregion

        #region Protected Methods

        protected override void NextCommandCore()
        {
            base.NextCommandCore();
            if (SelectedItem == null)
            {
                ErrorMessage = "You have to specify a product.";
                return;
            }
            
            Reminder.ProductModel = SelectedItem;
            switch (SelectedItem.Type)
            {
                case ReminderSubType.Product:
                    ShowValidationViewModel<PetReminderProductDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                    {
                        Reminder = Reminder,
                        PetReminderModel = Reminder.PetReminderModel//,
                      //  Parent = this,
                    });
                    break;
                case ReminderSubType.Other:
                    ShowValidationViewModel<PetReminderOtherDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                    {
                        Reminder = Reminder,
                        PetReminderModel = Reminder.PetReminderModel
                        //Parent = this,
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region LifeCycle

        protected override async Task<bool> LoadDataAsync()
        {
            var products = await _reminderService.GetProducts(Reminder.Type);
            Items = new ObservableCollection<IItemRadio>(products);
            return true;
        }

        protected override void RealInit(PetReminderSelectProductParameter parameter)
        {
            Reminder = parameter.Reminder;
            _parameter = parameter;
            LoadDataAsync();
        }

        #endregion
    }

    public class PetReminderSelectProductParameter : PetReminderListParameter
    {
        public ReminderModel Reminder;
    }
}
