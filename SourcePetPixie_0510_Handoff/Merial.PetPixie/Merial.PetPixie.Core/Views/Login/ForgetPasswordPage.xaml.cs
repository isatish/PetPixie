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
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class ForgetPasswordPage : ContentPage
    {
     

 


        public ForgetPasswordPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();


            MessagingCenter.Subscribe<ForgetPasswordViewModel>(this, "ShowLoadingMessage", (sender) =>
			{
                        var loader = new Shared.Loader();

                        loader.SetLoadingMessage("Processing Password Request");
                        stackLoader.Children.Clear();
                        stackLoader.Children.Add(loader);
                        });
                }


        public void SetNavigationBarProperties()
        {
           // try
           // {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("Forgot Your Password?");
            navBar.ShowBackCommand();
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
