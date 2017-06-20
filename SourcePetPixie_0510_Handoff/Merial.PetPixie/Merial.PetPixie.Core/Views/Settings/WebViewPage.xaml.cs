using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using Merial.PetPixie.Core.ViewModels;
using FAB.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class WebViewPage : ContentPage
    {


        public WebViewPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
          
        }
        public void SetNavigationBarProperties()
        {
                NavigationPage.SetHasNavigationBar(this, false);
          
                navBar.ShowBackCommand();
        }


    }
}
