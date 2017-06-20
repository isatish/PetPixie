using Merial.PetPixie;
using Merial.PetPixie.iOS;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Merial.PetPixie.Core.Views;
using Foundation;

[assembly: ExportRenderer(typeof(GestureImage), typeof(GestureImageRenderer))]
namespace Merial.PetPixie.iOS
{
    public class AnimatedImage : Image
    {
        public static readonly BindableProperty AnimateProperty = BindableProperty.Create(
            propertyName: "Animate",
            returnType: typeof(bool),
            declaringType: typeof(AnimatedImage),
            defaultValue: false);

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }
    }

    public class GestureImageRenderer : ImageRenderer
    {
        UILongPressGestureRecognizer longPressGestureRecognizer;
        UIPinchGestureRecognizer pinchGestureRecognizer;
        UIPanGestureRecognizer panGestureRecognizer;
        UISwipeGestureRecognizer swipeGestureRecognizer;
        UIRotationGestureRecognizer rotationGestureRecognizer;


        const int imageCount = 10;
        NSMutableArray imageArray;
        public GestureImageRenderer()
        {
            imageArray = new NSMutableArray(imageCount);
            for (int i = 0; i < imageCount; i++)
                imageArray.Add(UIImage.FromFile(new NSString($"small_green_loader_{i}.png")));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            longPressGestureRecognizer = new UILongPressGestureRecognizer(() => Console.WriteLine("Long Press"));
            pinchGestureRecognizer = new UIPinchGestureRecognizer(() => Console.WriteLine("Pinch"));
            //panGestureRecognizer = new UIPanGestureRecognizer (() => Console.WriteLine ("Pan"));

        //    swipeRightGestureRecognizer = new UISwipeGestureRecognizer(() => UpdateRight()) { Direction = UISwipeGestureRecognizerDirection.Right };
        //    swipeLeftGestureRecognizer = new UISwipeGestureRecognizer(() => UpdateLeft()) { Direction = UISwipeGestureRecognizerDirection.Left };
            rotationGestureRecognizer = new UIRotationGestureRecognizer(() => Console.WriteLine("Rotation"));

            if (e.NewElement == null)
            {
                if (longPressGestureRecognizer != null)
                {
                    this.RemoveGestureRecognizer(longPressGestureRecognizer);
                }
                if (pinchGestureRecognizer != null)
                {
                    this.RemoveGestureRecognizer(pinchGestureRecognizer);
                }

                /*
                if (panGestureRecognizer != null) {
                    this.RemoveGestureRecognizer (panGestureRecognizer);
                }
                */

                //if (swipeRightGestureRecognizer != null)
                //{
                //    this.RemoveGestureRecognizer(swipeRightGestureRecognizer);
                //}
                //if (swipeLeftGestureRecognizer != null)
                //{
                //    this.RemoveGestureRecognizer(swipeLeftGestureRecognizer);
                //}

                if (rotationGestureRecognizer != null)
                {
                    this.RemoveGestureRecognizer(rotationGestureRecognizer);
                }
            }

            if (e.OldElement == null)
            {
                this.AddGestureRecognizer(longPressGestureRecognizer);
                this.AddGestureRecognizer(pinchGestureRecognizer);
                //this.AddGestureRecognizer (panGestureRecognizer);
              //  this.AddGestureRecognizer(swipeRightGestureRecognizer);
              //  this.AddGestureRecognizer(swipeLeftGestureRecognizer);
                this.AddGestureRecognizer(rotationGestureRecognizer);
            }
        }

        private void UpdateLeft()
        {
            MessagingCenter.Send("Swiped to the left", "LeftSwipe");

        }
        private void UpdateRight()
        {
            // same as for left, but flipped
            MessagingCenter.Send("Swiped to the Right", "RightSwipe");

        }

        //protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        //{
        //    longPressGestureRecognizer = new UILongPressGestureRecognizer(() => Console.WriteLine("Long Press"));
        //    pinchGestureRecognizer = new UIPinchGestureRecognizer(() => Console.WriteLine("Pinch"));
        //    //panGestureRecognizer = new UIPanGestureRecognizer (() => Console.WriteLine ("Pan"));

        //    rotationGestureRecognizer = new UIRotationGestureRecognizer(() => Console.WriteLine("Rotation"));



        //    base.OnElementChanged(e);
        //    if (Control != null)
        //    {
        //        Control.AnimationImages = NSArray.FromArray<UIImage>(imageArray);
        //        Control.AnimationDuration = 1;
        //        Control.AnimationRepeatCount = 0;

              
        //        if (e.NewElement != null)
        //        {
                    
        //            //if ((e.NewElement as AnimatedImage).Animate)
        //            //    Control.StartAnimating();
        //        }

        //    }
            

        //    if (e.OldElement == null)
        //    {
               
        //    }
        //}



        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Animate")
            {
                if ((sender as AnimatedImage).Animate)
                    Control?.StartAnimating();
                else
                    Control?.StopAnimating();
            }

        }

    }
}
 


//[assembly: ExportRenderer(typeof(GestureImage), typeof(GestureImageRenderer))]
//namespace Merial.PetPixie.iOS
//    {
//        public class GestureImageRenderer : ImageRenderer
//        {
//        UILongPressGestureRecognizer longPressGestureRecognizer;
//        UIPinchGestureRecognizer pinchGestureRecognizer;
//        UIPanGestureRecognizer panGestureRecognizer;
//        UISwipeGestureRecognizer swipeGestureRecognizer;
//        UIRotationGestureRecognizer rotationGestureRecognizer;

//            protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//            {
//                base.OnElementPropertyChanged(sender, e);

 
//                if (longPressGestureRecognizer != null)
//                {
//                    this.RemoveGestureRecognizer(longPressGestureRecognizer);
//                }
//                if (pinchGestureRecognizer != null)
//                {
//                    this.RemoveGestureRecognizer(pinchGestureRecognizer);
//                }
//                if (panGestureRecognizer != null)
//                {
//                    this.RemoveGestureRecognizer(panGestureRecognizer);
//                }
//                if (swipeGestureRecognizer != null)
//                {
//                    this.RemoveGestureRecognizer(swipeGestureRecognizer);
//                }
//                if (rotationGestureRecognizer != null)
//                {
//                    this.RemoveGestureRecognizer(rotationGestureRecognizer);
//                }
           

 
//                this.AddGestureRecognizer(longPressGestureRecognizer);
//                this.AddGestureRecognizer(pinchGestureRecognizer);
//                this.AddGestureRecognizer(panGestureRecognizer);
//                this.AddGestureRecognizer(swipeGestureRecognizer);
//                this.AddGestureRecognizer(rotationGestureRecognizer);
           
//            }
//        }


//}

