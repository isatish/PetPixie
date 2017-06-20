using System.Threading.Tasks;
using KinveyXamarin;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IUploadService
    {
        Task<FileMetaData> UploadImage(byte[] fileBytes);
		Task<FileMetaData> UploadVideo(byte[] fileBytes);
    }
}
