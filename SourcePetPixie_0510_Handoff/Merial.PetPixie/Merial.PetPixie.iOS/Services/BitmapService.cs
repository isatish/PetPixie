using System;
using Common.Logging;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using Splat;


namespace Merial.PetPixie.iOS.Services
{
    public class BitmapService :IBitmapService
    {
        private IBitmap _bitmap;
        
        public IBitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public void SetBitmap(IBitmap bitmap)
        {
            if (bitmap != null)
                Bitmap = bitmap;
        }

        public IBitmap GetBitmap()
        {
            return Bitmap;
        }

        public void Reset()
        {
            if (_bitmap != null)
            {
                Bitmap.Dispose();
            }
        }



        public IBitmap GetBitmap(string path)
        {
			//System.IO.Stream ins = null;

			try 
			{
                return GetBitmap(path);
			}
			catch (Exception e)
            {
                var logger = Mvx.Resolve<ILog>();
                logger.ErrorFormat("[{0}] - {1}", GetType().Name, e.Message);
                Console.Error.WriteLine(GetType().Name, e.Message);
            }

            return null;
        }

		//Method not Needed in iOS
        //private static Android.Net.Uri GetImageUri(String path)
        //{
        //    /return Android.Net.Uri.FromFile(new Java.IO.File(path));
        //}

    }
}