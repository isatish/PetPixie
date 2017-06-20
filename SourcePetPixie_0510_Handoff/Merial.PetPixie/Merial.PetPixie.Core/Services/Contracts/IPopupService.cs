using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
	public interface IPopupService
    {
		void DisplayMessageAsToast(string message);
		void DisplayMessage(string message, string title);
        Task<bool> DisplayYesNoMessage(string title, string question, string actionYes, string actionNo);
        Task DisplayWorkInProgressAsync(string message = null);
	    Task DisplayMessageAsync(string title, string message);
	}
}