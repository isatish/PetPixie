using System;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.Core.Services
{
    public class UploadService : KinveyServiceBase, IUploadService
    {

        public UploadService(IKinveyService kinveyService) : base(kinveyService)
        {
        }

        public Task<FileMetaData> UploadImage(byte[] fileBytes)
        {
            var tsc = new TaskCompletionSource<FileMetaData>();

            var fileMetaData = new FileMetaData();
            fileMetaData._public = true;
            //fileMetaData.acl = new AccessControlList();
            //fileMetaData.acl.globallyReadable = true;

            try
            {
                KinveyService.GetClient().File().upload(fileMetaData, fileBytes, new KinveyDelegate<FileMetaData>()
                {
                    onSuccess = async (metadata) =>
                    {
                        var metadataDownload = await KinveyService.GetClient().File().downloadMetadataAsync(metadata.id);
                        tsc.SetResult(metadataDownload);
                    },
                    onError = (error) =>
                    {
                        tsc.SetException(error);
                    }
                });
            }
            catch (Exception e)
            {
                //Ignore
                _logger.Error(e);
                tsc.SetException(e);
            }

            return tsc.Task;
        }
		public Task<FileMetaData> UploadVideo(byte[] fileBytes)
		{
			var tsc = new TaskCompletionSource<FileMetaData>();

			var fileMetaData = new FileMetaData();
			fileMetaData._public = true;
			fileMetaData.fileName = System.Guid.NewGuid() + ".mp4";
			//fileMetaData.acl = new AccessControlList();
			//fileMetaData.acl.globallyReadable = true;

			try
			{
				KinveyService.GetClient().File().upload(fileMetaData, fileBytes, new KinveyDelegate<FileMetaData>()
				{
					
					onSuccess = async (metadata) =>
					{
						var metadataDownload = await KinveyService.GetClient().File().downloadMetadataAsync(metadata.id);
						tsc.SetResult(metadataDownload);
					},
					onError = (error) =>
					{
						tsc.SetException(error);
					}
				});
			}
			catch (Exception e)
			{
				//Ignore
				_logger.Error(e);
				tsc.SetException(e);
			}

			return tsc.Task;
		}
    }
}