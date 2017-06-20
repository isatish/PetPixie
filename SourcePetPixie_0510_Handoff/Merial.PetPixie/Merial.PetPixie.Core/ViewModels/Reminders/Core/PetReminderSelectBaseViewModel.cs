using System.Collections.ObjectModel;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels.Reminder.Core
{
    public class PetReminderSelectBaseViewModel<TInit> : ViewModelBase<TInit>
    {
        #region Fields

        protected readonly IReminderService _reminderService;

        private ObservableCollection<IItemRadio> _items;
       
        private bool _showInputOtherValue = false;
        private string _errorMessage = "";

        #endregion

        #region Constructors

        public PetReminderSelectBaseViewModel(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        #endregion

        #region Public Properties

        public bool ShowInputOtherValue
        {
            get { return _showInputOtherValue; }
            set
            {
                _showInputOtherValue = value;
                RaisePropertyChanged(() => ShowInputOtherValue);
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

       
        public ObservableCollection<IItemRadio> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        
        #endregion

        #region Commandes

        public IMvxCommand NextCommand => new MvxCommand(NextCommandCore);
        public IMvxCommand CloseCommand => new MvxCommand(CloseCommandeCore);

        protected virtual void NextCommandCore()
        {
        }

        protected virtual void CloseCommandeCore()
        {
            Close(this);
        }

        protected override void RealInit(TInit parameter)
        {
        }

        #endregion
    }
}
