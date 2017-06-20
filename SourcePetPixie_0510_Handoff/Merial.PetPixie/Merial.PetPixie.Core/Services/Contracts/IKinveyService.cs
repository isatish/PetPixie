using KinveyXamarin;
using SQLite.Net.Interop;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IKinveyService
    {
        void CreateClient(string filePath, ISQLitePlatform platform);
        Client GetClient();
    }
}