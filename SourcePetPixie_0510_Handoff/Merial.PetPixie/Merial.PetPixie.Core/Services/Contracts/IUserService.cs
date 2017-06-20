using System.Threading.Tasks;
using KinveyXamarin;

namespace Merial.PetPixie.Core.Services.Contracts {
    public interface IUserService
    {
        UserType UserType { get; }
        Task<User> LoginAutomatedAsync(string username, string password);
        Task<bool> EmailExistAsync(string email);
        void Logout();
        Task<KinveyDeleteResponse> DeleteMeAsync();
        string GetCurrentUserId();
        bool UserConnected();
        Task<User> SignupAsync(string username, string password, string email);
        Task<User> ChangeEmailAsync(User user, string newMail);
        string GetProfileId();
        Task<bool> SendResetPasswordInstructionsAsync(string email);
    }
}