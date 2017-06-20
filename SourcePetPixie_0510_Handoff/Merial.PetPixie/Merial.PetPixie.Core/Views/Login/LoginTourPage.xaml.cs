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
using System.Collections.Generic;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class LoginTourPage : ContentPage
    {
     

 


        public LoginTourPage()
        {
            InitializeComponent();
            this.MyCarouselView.ItemsSource = new List<int> { 1, 2, 3, 4 };
            this.MyCarouselView.Position = 0;
            this.MyCarouselView.BackgroundColor = Color.Transparent;
            SetNavigationBarProperties();
        }
        public void SetNavigationBarProperties()
        {
           // try
           // {
                NavigationPage.SetHasNavigationBar(this, false);
          //     navBar.ShowHeaderText("Sign Up");

             //   var title = AppResources.AboutTitle;
             //   navBar.ShowHeaderText(title);
             //   navBar.ShowBack();
           // }
          //  catch (Exception exc)
          //  {
          //      ExceptionHelper.HandleException(exc);
          //  }

        }
    }
}
