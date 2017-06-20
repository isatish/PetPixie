using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.Plugins
{
    public interface IDeviceHelper
    {
        ContactModel[] GetContacts();
        void HideKeyBoard();
    }
}