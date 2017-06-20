using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Resx;
using Xamarin.Forms;

namespace Merial.PetPixie.Core
{
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
        }

        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("About Pet+Pixie");

                var title =  AppResources.AboutTitle;
                navBar.ShowHeaderText(title);
                navBar.ShowBack();
            }
            catch (Exception exc)
            {
                ExceptionHelper.HandleException(exc);
            }

        }

 
    }
   
}
