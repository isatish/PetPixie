using System;
using System.IO;
using KinveyXamarin;
using Merial.PetPixie.Core;
//using Merial.PetPixie.iOS.Core.Crop;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Platform;
using MvvmCross.Platform.iOS.Views;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using System.Threading.Tasks;
using MvvmCross.Platform.Core;
using Foundation;
using Merial.PetPixie.Core.Views;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
//using Squareup.Picasso;
//using MvxIosTask = MvvmCross.iOS.Platform.MvxIosTask;

namespace Merial.PetPixie.iOS.Services
{
    public class ImageService : MvxIosTask, Merial.PetPixie.Core.Services.Contracts.IImageService
    {

        UIImageView imageView;
        CropperView cropperView;
        UIPanGestureRecognizer pan;
        UIPinchGestureRecognizer pinch;
        UITapGestureRecognizer doubleTap;


        public const int CropImage = 29;
        private RequestParameters _currentRequestParameters;

        public static UIImage BytesToImage( byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            UIImage image = null;
            try
            {

                image = new UIImage(NSData.FromArray(data));


               // image = ResizeImage(data, 400, 400);

                data = null;
            }
            catch (Exception)
            {
                return null;
            }
            return image;
        }



        public byte[] ImageToBytes(UIImage image)
        {
            Byte[] myByteArray;

            using (NSData imageData = image.AsPNG())
            {
                myByteArray = new Byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
            }

            return myByteArray;
        }

        UIImage ResizeImage(UIImage imageSource, float width, float height)
        {
                        //resize image
            var imageBytes = ImageToBytes(imageSource);
            imageBytes = ResizeImage(imageBytes, width, height);

            var imageResized = BytesToImage(imageBytes);
           

            return imageResized;

        }


            

        public byte[] MergePictures(byte[] imageBackground, List<FunMetadata> funImages)
        {
            UIWindow window;
            UIImage imageBG;
            UIImage imageFunItem;
            UIImage combinedImage;
            UIImageView combinedImageView;

            window = new UIWindow(UIScreen.MainScreen.Bounds);

            imageBG = BytesToImage(imageBackground);

            //imageBG = ResizeImage(imageBG, 320, 320);

           // UIGraphics.BeginImageContext(UIScreen.MainScreen.Bounds.Size);


            UIGraphics.BeginImageContextWithOptions(new SizeF(300, 300), false, 1);



            //Remember, the size of the screen is set in points 320x480 (iPhone 4)  320x568 (iPhone 5) 
          //  imageBG.Draw(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Width));
            imageBG.Draw(new CGRect(0, 0, 300, 300));
        //    imageBG.Draw(new CGPoint(0,0));

            foreach (FunMetadata metaData in funImages)
            {

                imageFunItem = UIImage.FromFile(metaData.ItemImage);


                if (metaData.ItemRotation > 0)
                {
                     imageFunItem = RotateImage(imageFunItem, metaData.ItemRotation, metaData.ItemWidth, metaData.ItemHeight, metaData.ItemScale);
                }


                imageFunItem.Draw(new CGRect(metaData.ItemX, metaData.ItemY, (75 * metaData.ItemScale), (75 * metaData.ItemScale)));//UIScreen.MainScreen.Bounds.Width / 4, UIScreen.MainScreen.Bounds.Width / 4, metaData.ItemWidth/2, metaData.ItemHeight/2));


            }

            combinedImage = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            using (NSData imageData = combinedImage.AsPNG()) //.AsPNG())
            {
                Byte[] myByteArray = new Byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
                return myByteArray;
            }
        }




      //  public byte[] MergePicturesOrig(byte[] imageBackground, FunMetadata metaData)
      //  {
      //      UIWindow window;
      //      UIImage image1;
      //      UIImage image2;
      //      UIImage combinedImage;
      //      UIImageView combinedImageView;


      //      window = new UIWindow(UIScreen.MainScreen.Bounds);

      //      //   viewController = new MergeImagesViewController();

      //      // image1 = UIImage.FromFile("monkey1.png");



      //    //ds  imageBackground = ResizeImage(imageBackground, 320, 320);

      //      image1 = BytesToImage(imageBackground);

      //     //ds var funImageFile = metaData.ItemImage.Replace("thumb_", "");
      //      image2 = UIImage.FromFile(metaData.ItemImage);




      //      //resize image
      //      // var image2Bytes = ImageToBytes(image2);
      //      //image2Bytes = ResizeImage(image2Bytes, metaData.ItemWidth, metaData.ItemHeight);

      //      // image2 = BytesToImage(image2Bytes);
      //      //    image2 = ResizeImage(image2, metaData.ItemWidth, metaData.ItemHeight);
      //      //    image2.BackgroundColor = UIColor.Clear;


      //      //        image2 = ResizeImage(image2, 300, 300);

      //      //   if (metaData.ItemRotation > 0)
      //      //  {


      //      var rotation = metaData.ItemRotation;
      //      if (rotation < 0)
      //      {
      //          rotation = 360 + rotation;
      //      }


      ////ds        image2 = RotateImage(image2, rotation, 75, 75, metaData.ItemScale);
      //     //   image2 = RotateImage(image2, 45, metaData.ItemWidth, metaData.ItemHeight);
      //    //  }

      //      UIGraphics.BeginImageContext(UIScreen.MainScreen.Bounds.Size);

      //      //Remember, the size of the screen is set in points 320x480 (iPhone 4)  320x568 (iPhone 5) 
      //      image1.Draw(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Width));


      //      //   metaData.ItemX = 160 - 25;// 147.5;
      //      //   metaData.ItemY = 160 - 25;// 147.5;

      //      metaData.ItemX -= 20;// 75/2;// metaData.ItemX - metaData.ItemWidth/2;
      //      metaData.ItemY += 20;// (74/4);X







      //      // image2.Draw(new CGRect(50, 50, 75, 75));//UIScreen.MainScreen.Bounds.Width / 4, UIScreen.MainScreen.Bounds.Width / 4, metaData.ItemWidth/2, metaData.ItemHeight/2));
          

      //     // image2.Draw(new CGRect(metaData.ItemX, metaData.ItemY, metaData.ItemWidth, metaData.ItemHeight));//UIScreen.MainScreen.Bounds.Width / 4, UIScreen.MainScreen.Bounds.Width / 4, metaData.ItemWidth/2, metaData.ItemHeight/2));

      //      image2.Draw(new CGRect(metaData.ItemX, metaData.ItemY, (75 * metaData.ItemScale), (75 * metaData.ItemScale)));//UIScreen.MainScreen.Bounds.Width / 4, UIScreen.MainScreen.Bounds.Width / 4, metaData.ItemWidth/2, metaData.ItemHeight/2));

      //      if (metaData.ItemRotation > 0)
      //      {
      //         // combinedImageView.Transform = CGAffineTransform.MakeRotation((float)Math.PI / 4);
      //      }




      //      combinedImage = UIGraphics.GetImageFromCurrentImageContext();

      //      UIGraphics.EndImageContext();
                        
      //      using (NSData imageData = combinedImage.AsPNG()) //.AsPNG())
      //      {
      //          Byte[] myByteArray = new Byte[imageData.Length];
      //          System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
      //          return myByteArray;
      //      }


      //  }

        public UIImage RotateImage(UIImage originalImage, UIImageOrientation orientation)
        {
            return UIImage.FromImage(originalImage.CGImage, 1, orientation);
        }
    

        public UIImage RotateImage(UIImage originalImage, double rotationAngle, nfloat width, nfloat height, nfloat scale )
        {
            UIImage rotatedImage = originalImage;
            nfloat imageWidth = originalImage.Size.Width;
            nfloat imageHeight = originalImage.Size.Height;
            nfloat imageScale = scale;

            imageWidth = 75; //width;
            imageHeight = 75; // height;
            //imageScale = 1;//scale;

            if (rotationAngle > 0)
            {
                CGSize rotatedSize;
                float angle = Convert.ToSingle((Math.PI / 180) * rotationAngle);

                //using (UIView rotatedViewBox = new UIView(new CGRect(0, 0, originalImage.Size.Width, originalImage.Size.Height)))

                 using (UIView rotatedViewBox = new UIView(new CGRect(0, 0, imageWidth, imageHeight)))
                {
                    CGAffineTransform t = CGAffineTransform.MakeRotation(angle);
                    rotatedViewBox.Transform = t;
                    rotatedSize = rotatedViewBox.Frame.Size;

                    UIGraphics.BeginImageContext(rotatedSize);
                    CGContext context = UIGraphics.GetCurrentContext();

                    context.TranslateCTM(rotatedSize.Width / 2, rotatedSize.Height / 2);
                    context.RotateCTM(angle);


                   // context.ScaleCTM((nfloat)1.0, -(nfloat)1.0);
                     context.ScaleCTM((nfloat)imageScale, -(nfloat)imageScale);

                    context.DrawImage(new CGRect(-imageWidth / 2, -imageHeight / 2, imageWidth, imageHeight), originalImage.CGImage);

                    rotatedImage = UIGraphics.GetImageFromCurrentImageContext();

                    UIGraphics.EndImageContext();
                }

            }

            return rotatedImage;
        }

        public static byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            UIImage originalImage = ImageFromByteArray(imageData);
            //
            var Hoehe = originalImage.Size.Height;
            var Breite = originalImage.Size.Width;
            //
            nfloat ZielHoehe = 0;
            nfloat ZielBreite = 0;
            //

            if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
            {
                ZielHoehe = height;
                nfloat teiler = Hoehe / height;
                ZielBreite = Breite / teiler;
            }
            else // Breite (61 for Avatar) ist Master
            {
                ZielBreite = width;
                nfloat teiler = Breite / width;
                ZielHoehe = Hoehe / teiler;
            }
            //
            width = (float)ZielBreite;
            height = (float)ZielHoehe;
            //
            UIGraphics.BeginImageContext(new SizeF(width, height));
            originalImage.Draw(new RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            //
            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            //
            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }


        public void CropPicture(Stream imageStream, Action<FileMetaData> pictureAvailable, Action assumeCancelled, bool useTempFile = false)
        {

            //this.AddSubview(new CropperView());

            DisplayWorkInProgressAsync("abcde");

        }

                
        public Task DisplayWorkInProgressAsync(string message = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var alert = new UIAlertView();
            alert.Title = "Hello";
            alert.Message = message ?? "this is a message";// WorkInProgressDefaultMessage;
            alert.AddButton("OK");

            //Make the call thread-safe.
            Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction(alert.Show);
            alert.Clicked += (s, e) => { tcs.SetResult(true); };
            return tcs.Task;
        }

		public void CropPictureHold(Stream imageStream, Action<FileMetaData> pictureAvailable, Action assumeCancelled, bool useTempFile = false)
		{
            UIViewController vc = new UIViewController();
            //vc.PresentModalViewController(new cropperView(),true, null);

            //PresentViewController(auth.GetUI(), true, null);


             
            //ds throw new NotImplementedException();
            using (var image = UIImage.FromFile("VetProfileImage.png"))
            {
                imageView = new UIImageView(new CGRect(0, 0, image.Size.Width, image.Size.Height));
          //ds      imageView.Image = image;
            }

            cropperView = new CropperView { Frame = this.imageView.Frame };
          //DS  View.AddSubviews(imageView, cropperView);
           this.imageView.AddSubviews(imageView, cropperView);

            nfloat dx = 0;
            nfloat dy = 0;

            pan = new UIPanGestureRecognizer(() =>
            {
                if ((pan.State == UIGestureRecognizerState.Began || pan.State == UIGestureRecognizerState.Changed) && (pan.NumberOfTouches == 1))
                {

                   //ds var p0 = pan.LocationInView(View);
                     var p0 = pan.LocationInView(this.imageView);




                    if (dx == 0)
                        dx = p0.X - cropperView.Origin.X;

                    if (dy == 0)
                        dy = p0.Y - cropperView.Origin.Y;

                    var p1 = new CGPoint(p0.X - dx, p0.Y - dy);

                    cropperView.Origin = p1;
                }
                else if (pan.State == UIGestureRecognizerState.Ended)
                {
                    dx = 0;
                    dy = 0;
                }
            });

            nfloat s0 = 1;

            pinch = new UIPinchGestureRecognizer(() =>
            {
                nfloat s = pinch.Scale;
                nfloat ds = (nfloat)Math.Abs(s - s0);
                nfloat sf = 0;
                const float rate = 0.5f;

                if (s >= s0)
                {
                    sf = 1 + ds * rate;
                }
                else if (s < s0)
                {
                    sf = 1 - ds * rate;
                }
                s0 = s;

                cropperView.CropSize = new CGSize(cropperView.CropSize.Width * sf, cropperView.CropSize.Height * sf);

                if (pinch.State == UIGestureRecognizerState.Ended)
                {
                    s0 = 1;
                }
            });

            doubleTap = new UITapGestureRecognizer((gesture) => Crop())
            {
                NumberOfTapsRequired = 2,
                NumberOfTouchesRequired = 1
            };

            cropperView.AddGestureRecognizer(pan);
            cropperView.AddGestureRecognizer(pinch);
            cropperView.AddGestureRecognizer(doubleTap);






        }


                
        void Crop()
        {
            var inputCGImage = UIImage.FromFile("VetProfileImage.png").CGImage;

            var image = inputCGImage.WithImageInRect(cropperView.CropRect);
            using (var croppedImage = UIImage.FromImage(image))
            {

                imageView.Image = croppedImage;
                imageView.Frame = cropperView.CropRect;
               //ds imageView.Center = View.Center;
                 imageView.Center = this.imageView.Center;

                cropperView.Origin = new CGPoint(imageView.Frame.Left, imageView.Frame.Top);
                cropperView.Hidden = true;
            }
        }

		public void FetchImage(string url)
		{
			//ds throw new NotImplementedException();
		}

        public byte[] MergePictures(byte[] image, FunMetadata metaData)
        {
            throw new NotImplementedException();
        }

        //// Pedding Method
        //private MemoryStream LoadInMemoryBitmap(MvxIntentResultEventArgs result)
        //{
        //	var memoryStream = new MemoryStream();
        //	Bitmap bitmap = (Bitmap)result.Data.Extras.GetParcelable(Constants.ImageService.ExtraData);
        //	if (bitmap == null)
        //		return null;
        //	using (bitmap)
        //	{
        //		bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memoryStream);
        //	}
        //	memoryStream.Seek(0L, SeekOrigin.Begin);
        //	return memoryStream;
        //}

        private class RequestParameters
		{
			public RequestParameters(Action<FileMetaData> pictureAvailable,
									 Action assumeCancelled)
			{

				AssumeCancelled = assumeCancelled;
				PictureAvailable = pictureAvailable;
			}

			public Action<FileMetaData> PictureAvailable { get; private set; }
			public Action AssumeCancelled { get; private set; }

		}
	}


}