using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Common.Logging;
using MvvmCross.Core.ViewModels;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using MvvmCross.Platform.ExtensionMethods;

namespace Merial.PetPixie.Core.Helpers
{
    public class SafeMvxCommand<T> : MvxCommandBase, IMvxCommand, ICommand
    {
        protected readonly IPopupService _popupService;
        protected readonly ILog _logger;

        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        protected string _callMemberName;
        
        public SafeMvxCommand(Action<T> execute, [CallerMemberName] string callMemberName = null) : this(execute, null, callMemberName)
        {
        }

        public SafeMvxCommand(Action<T> execute, Func<T, bool> canExecute, [CallerMemberName] string callMemberName = null) : this(canExecute, callMemberName)
        {
            this._execute = execute;
        }

        protected SafeMvxCommand(Func<T, bool> canExecute, string callMemberName)
        {
            this._callMemberName = callMemberName;
            this._canExecute = canExecute;
            this._popupService = Mvx.Resolve<IPopupService>();
            this._logger = Mvx.Resolve<ILog>();
        }


        public bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
                return this._canExecute((T)MvxCrossCoreExtensions.MakeSafeValueCore(typeof(T), parameter));
            return true;
        }

        public bool CanExecute()
        {
            return this.CanExecute((object)null);
        }
        
        public virtual void Execute(object parameter)
        {
            try
            {
                if (!this.CanExecute(parameter))
                    return;
                _logger.Trace($"[ACTION] : Executing {this._callMemberName} command");
                this._execute((T)MvxCrossCoreExtensions.MakeSafeValueCore(typeof(T), parameter));
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _popupService.DisplayMessage("An error occured. Please try again", "Error");
            }
        }

        public virtual void Execute()
        {
            this.Execute(null);
        }
    }
}
