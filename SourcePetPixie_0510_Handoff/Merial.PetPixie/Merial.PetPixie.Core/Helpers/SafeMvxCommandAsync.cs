using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Helpers
{
    public class SafeMvxCommandAsync : SafeMvxCommand
    {
        private readonly Func<Task> _execute;
        
        public SafeMvxCommandAsync(Func<Task> execute, [CallerMemberName] string callMemberName = null) : this(execute, null, callMemberName)
        {
        }

        public SafeMvxCommandAsync(Func<Task> execute, Func<bool> canExecute, [CallerMemberName] string callMemberName = null) : base(canExecute, callMemberName)
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
                await this._execute();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _popupService.DisplayMessage("An error occured. Please try again", "Error");
            }
        }
    }
}
