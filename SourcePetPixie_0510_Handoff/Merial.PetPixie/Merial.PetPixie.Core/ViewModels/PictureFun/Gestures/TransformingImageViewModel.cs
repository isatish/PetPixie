using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
    public class TransformImageViewModel : TransformViewModel
    {


        public double _currentLeft = 150 - 37;
        public double _currentTop = 72.14;
        public double _currentScale = 1;
        public double _currentAngle = 0;
        public double currentX = 0;
        public double currentY = 0;
        public bool _isActive = true;
       


        //protected readonly string Path = Device.OnPlatform("images/", "", "Resources/images/");
        protected readonly string Path =
            Device.OS == TargetPlatform.iOS ? "images/" :
            Device.OS == TargetPlatform.Android ? "" : "Resources/images/";

        protected string[] images = new[] { "hat_1.png", "hat_2.png", "hat_3.png" };
        //protected string[] images = new[] { "Pic1.png", "Pic2.png", "Pic3.png", "Pic4.png" };
        protected int currentImage = 2;
        public string ImageSource
        {
            get { return Path + images[currentImage]; }
        }




        public string CropImageSource
        {
            get { return Settings.CurrentFunImageSource; }

        }




                
        public IMvxCommand AddAccessoryCommand => new SafeMvxCommand(AddAccessory);

        private void AddAccessory()
        {

            currentImage = 1;// "hat_3.png";
        }




        protected override void OnSwiped(MR.Gestures.SwipeEventArgs e)
        {
          
            if (this.IsActive)
            {
                base.OnSwiped(e);

                if (e.Direction == MR.Gestures.Direction.Right)
                {
                    currentImage--;
                    if (currentImage < 0)
                        currentImage = images.Length - 1;
                    NotifyPropertyChanged(() => ImageSource);
                }
                else if (e.Direction == MR.Gestures.Direction.Left)
                {
                    currentImage++;
                    if (currentImage >= images.Length)
                        currentImage = 0;
                    NotifyPropertyChanged(() => ImageSource);
                }
            }
        }



        #region Properties

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                //RaisePropertyChanged(() => CurrentLeft);
            }
        }


        public double CurrentAngle
        {
            get { return _currentAngle; }
            set
            {
                if (this.IsActive)
                {
                    _currentAngle = value;
                    //RaisePropertyChanged(() => CurrentScale);
                }
            }
        }



        public double CurrentScale
        {
            get { return _currentScale; }
            set
            {
                if (this.IsActive)
                {
                    _currentScale = value;
                    //RaisePropertyChanged(() => CurrentScale);
                }
            }
        }

        public double CurrentLeft
        {
            get { return _currentLeft; }
            set
            {
                if (this.IsActive)
                {
                    _currentLeft = value;
                    //RaisePropertyChanged(() => CurrentLeft);
                }
            }
        }


        public double CurrentTop
        {
            get { return _currentTop; }
            set
            {
                if (this.IsActive)
                {

                    _currentTop = value;
                    //      RaisePropertyChanged(() => CurrentTop);
                }
            }
        }





        #endregion

        public TransformImageViewModel()
            : base()
        {
            
          //  SetAnchor(new Point(150, 72.150));

            MessagingCenter.Subscribe<CustomEventArgsViewModel, MR.Gestures.PanEventArgs>(this, "ImagePanning", (sender, e) =>
            {
                if (this.IsActive)
                {
                    //   var newX = e.DeltaDistance.X;
                    CurrentLeft += e.DeltaDistance.X;// e.Velocity.X;
                    CurrentTop += e.DeltaDistance.Y;//  e.Velocity.Y;
                }
            });

            MessagingCenter.Subscribe<CustomEventArgsViewModel, MR.Gestures.PinchEventArgs>(this, "ImageScaled", (sender, e) =>
            {
                if (this.IsActive)
                {
                       CurrentScale = e.TotalScale;// e.Velocity.X;
                    //_currentScale += e.DeltaScale;
                }
            });


            MessagingCenter.Subscribe<CustomEventArgsViewModel, MR.Gestures.RotateEventArgs>(this, "Rotating", (sender, e) =>
            {
                if (this.IsActive)
                {
                    _currentAngle += e.DeltaAngle;
                    SetAnchor(e.Center);
                }
            });


        }
    }
}

