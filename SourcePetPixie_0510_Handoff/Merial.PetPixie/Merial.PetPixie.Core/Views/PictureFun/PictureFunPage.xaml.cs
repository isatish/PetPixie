using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Merial.PetPixie.Core.Helpers;
//using FFImageLoading.Transformations;
//using FFImageLoading;
//using FFImageLoading.Forms;
//using FFImageLoading.Work;
using System.IO;
//using FFImageLoading.Forms;
//using FFImageLoading.Work;
using System.Threading.Tasks;
using MvvmCross.Platform;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class PictureFunPage : ContentPage
    {
        public PictureFunPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
            this.MyCarouselView.ItemsSource = new List<int> { 1, 2, 3, 4 };
            this.MyCarouselView.Position = 0;
            this.MyCarouselView.BackgroundColor = Color.Transparent;

            FunMetadata metaData = new FunMetadata();


            List<FunMetadata> funImages = new List<FunMetadata>();

            byte[] bacgkroundImage = App.FunPictureBytes;


            //Initialize Background image size

            bacgkroundImage =  Mvx.Resolve<IImageService>().MergePictures(bacgkroundImage, funImages);


           imageOriginal.Source = ImageSource.FromStream(() => new MemoryStream(bacgkroundImage));     

          
            MessagingCenter.Subscribe<PictureFunViewModel>(this, "SaveFun", (sender) =>
            {

                //Initialize background with the original images taken/selected
                App.FunPictureUpdatedBytes = App.FunPictureBytes;

                   
                bacgkroundImage = App.FunPictureBytes;



                funImages = new List<FunMetadata>();


                //Initialize Background image size
                bacgkroundImage =  Mvx.Resolve<IImageService>().MergePictures(bacgkroundImage, funImages);

              


                //Add fun image to it one by one
                foreach (var fun in gridFun.Children)
                {
                    if (fun is PictureFunModView)
                    {
                        var currentFun = ((TransformImageViewModel)fun.BindingContext);

                        metaData = new FunMetadata();
                        metaData.ItemWidth = 75; 
                        metaData.ItemHeight = 75;
                        metaData.ItemRotation = currentFun.CurrentAngle;
                        metaData.ItemScale = float.Parse(currentFun.CurrentScale.ToString());
                        metaData.ItemX = currentFun.CurrentLeft;
                        metaData.ItemY = currentFun.CurrentTop;
                        metaData.ItemImage = ((PictureFunModView)fun).FunFileName;

                        funImages.Add(metaData);
                    }
                }


                App.FunPictureUpdatedBytes =  Mvx.Resolve<IImageService>().MergePictures(bacgkroundImage, funImages);


            });


          

            }


        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("have fun");
                navBar.ShowAcceptFunCommand();
                navBar.ShowBackCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }


        void Handle_Fun_Clicked(object sender, System.EventArgs e)
        {
            Button itemButton = (Button)sender;

            string fileName = itemButton.Image.File;


            //Set IsEnabled for existing ones so that they do not receive updates

            //Add fun image to it one by one
            foreach (var fun in gridFun.Children)
            {
                if (fun is PictureFunModView)
                {
                     ((TransformImageViewModel)fun.BindingContext).IsActive = false;
                }

            }

            var newFunItem = new PictureFunModView(fileName) { WidthRequest = 75, HeightRequest = 75 };
            newFunItem.BindingContext = new TransformImageViewModel();


            gridFun.Children.Add(newFunItem);//new PictureFunModView(fileName) { WidthRequest = 50, HeightRequest = 50 });
        }


        void Handle_ShowHats(object sender, System.EventArgs e)
        {
            MyCarouselView.Position = 1;
        }
        void Handle_ShowHair(object sender, System.EventArgs e)
        {
            MyCarouselView.Position = 2;
        }
        void Handle_ShowAccessories(object sender, System.EventArgs e)
        {
            MyCarouselView.Position = 3;
        }
        void Handle_ShowClothes(object sender, System.EventArgs e)
        {
            MyCarouselView.Position = 3;
        }

        void HideScreenElements()
        {
            StackFunItems.IsVisible = false;
            StackEdit.IsVisible = false;
            StackText.IsVisible = false;
            MyCarouselView.IsVisible = false;
            StackFunNavigation.IsVisible = false;
        }

        void Handle_ShowFunCarousel(object sender, System.EventArgs e)
        {
            HideScreenElements();
            StackFunItems.IsVisible = true;

            MyCarouselView.IsVisible = true;
            StackFunNavigation.IsVisible = true;
        }
        void Handle_ShowEdit(object sender, System.EventArgs e)
        {
            HideScreenElements();
            StackEdit.IsVisible = true;
        }
        void Handle_ShowText(object sender, System.EventArgs e)
        {
            HideScreenElements();
            StackText.IsVisible = true;
        }


      
    }
}
