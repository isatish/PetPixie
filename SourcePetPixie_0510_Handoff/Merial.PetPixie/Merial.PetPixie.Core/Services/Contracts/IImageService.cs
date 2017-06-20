using System;
using System.Collections.Generic;
using System.IO;
using KinveyXamarin;
using Merial.PetPixie.Core.Views;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IImageService
    {
        byte[] MergePictures(byte[] image, FunMetadata metaData);
        byte[] MergePictures(byte[] imageBackground, List<FunMetadata> funImages);
        void CropPicture(Stream image, Action<FileMetaData> pictureAvailable, Action assumeCancelled, bool useTempFile = false);
        void FetchImage(string url);
    }
}