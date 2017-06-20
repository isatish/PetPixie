using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels.Dialog
{
	public class RateDialogViewModel : ViewModelBase
    {
        #region Fields

        private readonly Action<int> _submitAction;
		private int _rate;

        #endregion

        #region Public Properties

        public int Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                RaisePropertyChanged(() => Rate);
            }
        }

        #endregion

        #region Public Methods

        public RateDialogViewModel(Action<int> submitAction)
        {
			_submitAction = submitAction;
			Rate = 3;
		}

        #endregion

        #region Commands

        public IMvxCommand RateCommand => new SafeMvxCommand<int>(OnRate);
		public IMvxCommand SubmitCommand => new SafeMvxCommand(OnSubmit);

        private void OnSubmit()
        {
            _submitAction.Invoke(Rate);
        }

        private void OnRate(int rate)
        {
            Rate = rate;
        }

        #endregion
    }
}