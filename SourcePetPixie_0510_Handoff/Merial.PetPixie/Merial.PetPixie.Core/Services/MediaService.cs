using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.Core.Services
{
    public class MediaService : KinveyServiceBase, IMediaService
    {
        private readonly IUserService userService;

		public MediaService(IKinveyService kinveyService, IUserService userService) : base(kinveyService)
		{
		    this.userService = userService;
		}

        public Task<KMedia> SharePicture(string description, IEnumerable<string> pets, FileMetaData fileMetaData)
        {
            var currentProfileId = this.userService.GetProfileId();
		    return base.SaveAppdataAsync("Media", new KMedia
		    {
		        Type = "media",
                KImage = new KImage
                {
                    Id = fileMetaData.id,
                    Type = "KinveyFile",
                    MimeType = "image/jpeg"
                },
                Caption = description ?? string.Empty,
                ProfileId = currentProfileId,
                UserId = this.userService.GetCurrentUserId(),
                PetIds = pets.ToList(),
                Comments = Enumerable.Empty<KComment>().ToList(),
                KVideos = Enumerable.Empty<object>().ToList(),
                KLikes = Enumerable.Empty<KLike>().ToList(),
                Geoloc = new List<double> { 32, -21 }
		    });
		}

		public Task<KMedia> ShareVideo(string description, IEnumerable<string> pets, FileMetaData fileMetaData)
		{
			var currentProfileId = this.userService.GetProfileId();
			return base.SaveAppdataAsync("Media", new KMedia
			{

				KVideo = new KVideo
				{
					Id = fileMetaData.id,
					Type = "KinveyFile",
					MimeType = "video/mp4"
				},
				Caption = description ?? string.Empty,
				ProfileId = currentProfileId,
				UserId = this.userService.GetCurrentUserId(),
				PetIds = pets.ToList(),
				Comments = Enumerable.Empty<KComment>().ToList(),
				KVideos = Enumerable.Empty<object>().ToList(),
				KLikes = Enumerable.Empty<KLike>().ToList(),
				Geoloc = new List<double> { 32, -21 }
			});
		}

        public Task<KMedia> GetMediaById(string mediaId)
        {
            return this.GetAppDataEntityAsync<KMedia>("Media", mediaId);
        }

        public Task<IEnumerable<KMedia>> GetMediaListById(IEnumerable<string> mediaIds)
        {
            return this.GetAppDataEntitesAsync<KMedia>("Media", mediaIds);
        }
	}
}