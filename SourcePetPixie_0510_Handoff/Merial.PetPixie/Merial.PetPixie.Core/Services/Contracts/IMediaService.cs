using System.Collections.Generic;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
	public interface IMediaService
    {
        Task<KMedia> SharePicture(string description, IEnumerable<string> pets, FileMetaData fileMetaData);
		Task<KMedia> ShareVideo(string description, IEnumerable<string> pets, FileMetaData fileMetaData);
	    Task<KMedia> GetMediaById(string mediaId);
	    Task<IEnumerable<KMedia>> GetMediaListById(IEnumerable<string> mediaIds);
	}
}