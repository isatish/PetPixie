using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Merial.PetPixie.Core.Helpers;

namespace Merial.PetPixie.Core.Views
{
    public partial class CropperPage : ContentPage
    {
       

        void Handle_Accessory1_Clicked(object sender, System.EventArgs e)
        {
            var currentImage = 1;

            gridFun.Children.Add(new PictureFunModView() { WidthRequest = 75, HeightRequest = 75 });


            //  funView1.DownCommand.Execute(null);
            //   funView2.UpCommand.Execute(null);
        }

        public IMvxCommand AddAccessoryCommand => new SafeMvxCommand(AddAccessory);

        private void AddAccessory()
        {




            var currentImage = 1;// "hat_3.png";
        }


        public CropperPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();

        }
        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("crop your photo");
                navBar.ShowAcceptCropCommand();
                navBar.ShowBackCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }



       
    }
}
