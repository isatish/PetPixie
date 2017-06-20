using System;
using System.Collections.ObjectModel;
using System.Linq;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
    public class SelectVetReminderViewModel : PetReminderSelectBaseViewModel<PetReminderSelectTypeParameter>
    {
        #region Fields

        private VetReminderModel _selectedItem;

        #endregion

        #region Constructor

        public SelectVetReminderViewModel(IReminderService reminderService,VetReminderModel vetModel) : base(reminderService)
        {
            Items =
                new ObservableCollection<IItemRadio>(Settings.CurrentUserProfile.Vets.Select(VetReminderModel.CreateFrom))
                {
                    new VetReminderModel
                    {
                        Id = null,
                        Name = "Other",
                        OtherValue = !string.IsNullOrEmpty(vetModel?.OtherValue) ? vetModel?.OtherValue : null
                    }
                };

            if (vetModel != null)
            {
                SelectedItem = vetModel;
                IndexSelectInitItem = Items.IndexOf(Items.FirstOrDefault(i => ((VetReminderModel)i).Id == vetModel.Id || i.Name == vetModel.Name));
            }
        }

        #endregion

        #region Events

        public event EventHandler<VetelectedEventArgs> ItemSelected;
        public event EventHandler CloseWindows;
        
        #endregion

        #region Public Properties

        public VetReminderModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                ItemSelectChange();
            }
        }

        public int IndexSelectInitItem { get; set; }

        #endregion

        #region Override methods

        protected override void NextCommandCore()
        {
            base.NextCommandCore();
            if (SelectedItem == null || (SelectedItem.Id == null && string.IsNullOrWhiteSpace(SelectedItem.OtherValue)))
            {
                ErrorMessage = "You have to specify a vet.";
                return;
            }

            ItemSelected?.Invoke(this,new VetelectedEventArgs(SelectedItem));
            CloseWindows?.Invoke(this, EventArgs.Empty);
        }

        protected override void CloseCommandeCore()
        {
            base.CloseCommandeCore();
            CloseWindows?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void ItemSelectChange()
        {
            ErrorMessage = "";
            ShowInputOtherValue = SelectedItem.Id == null;
        }

        #endregion

        public class VetelectedEventArgs : EventArgs
        {
            public VetelectedEventArgs(VetReminderModel vet)
            {
                Selection = vet;
            }

            public VetReminderModel Selection { get; private set; }
        }
    }
}
