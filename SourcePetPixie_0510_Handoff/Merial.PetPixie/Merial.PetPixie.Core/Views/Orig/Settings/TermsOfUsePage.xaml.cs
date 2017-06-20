using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Resx;
using Xamarin.Forms;

namespace Merial.PetPixie.Core
{
    public partial class TermsOfUsePage : ContentPage
    {
        public TermsOfUsePage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
        }

        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                var title = AppResources.TermsOfServiceTitle;
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
