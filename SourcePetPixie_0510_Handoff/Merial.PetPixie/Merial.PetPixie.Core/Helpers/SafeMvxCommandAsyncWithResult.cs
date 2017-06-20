using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MvvmCross.Platform.ExtensionMethods;

namespace Merial.PetPixie.Core.Helpers
{
    public class SafeMvxCommandAsync<T> : SafeMvxCommand<T>
    {
        private readonly Func<T,Task> _execute;
        
        public SafeMvxCommandAsync(Func<T,Task> execute, [CallerMemberName] string callMemberName = null) : this(execute, null, callMemberName)
        {
        }

        public SafeMvxCommandAsync(Func<T,Task> execute, Func<T,bool> canExecute, [CallerMemberName] string callMemberName = null) : base(canExecute, callMemberName)
        {
            this._execute = execute;
        }
        
        public override async void Execute(object parameter)
        {
            try
            {
                if (!this.CanExecute(parameter))
                    return;
                _logger.Trace($"[ACTION] : Executing {this._callMemberName} command");
                await this._execute((T)MvxCrossCoreExtensions.MakeSafeValueCore(typeof(T), parameter));
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _popupService.DisplayMessage("An error occured. Please try again", "Error");
            }
        }
    }
}
