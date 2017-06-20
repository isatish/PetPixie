using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.ViewModels;
using FAB.Forms;
using System;
using Plugin.Media;
using System.Collections.Generic;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class FeedItemPage : ContentPage
    {

  

        public FeedItemPage()
        {
            InitializeComponent();



            //OnAppearing += FeedPage_OnAppearing1;
            //  navBar.BindingContext = this.BindingContext;
            SetNavigationBarProperties();
            // SetFABProperties();
            //fab.Clicked += (sender, e) =>
            //{
            //    dialogPost.IsVisible = true;
            //    //  TakePhoto();
            //    MessagingCenter.Send(this, "FabClicked");

            //};
       
        }



        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("details");
                navBar.ShowFindFriendsCommand();
             //   navBar.ShowNotificationCommand();
                navBar.ShowBackCommand();

                //   navBar.ShowHeaderText("About Pet+Pixie");

                //   var title = AppResources.AboutTitle;
                //   navBar.ShowHeaderText(title);
                //   navBar.ShowBack();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }





    }
}
