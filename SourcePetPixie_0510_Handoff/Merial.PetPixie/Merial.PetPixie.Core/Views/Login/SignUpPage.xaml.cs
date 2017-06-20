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
    public partial class SignupPage : ContentPage
    {
     

 


        public SignupPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
        }

                public void SetNavigationBarProperties()
                {
                        // try
                        // {
                        NavigationPage.SetHasNavigationBar(this, false);
                        navBar.ShowHeaderText("Sign Up");

                        //   var title = AppResources.AboutTitle;
                        //   navBar.ShowHeaderText(title);
                        //   navBar.ShowBack();
                        // }
                        //  catch (Exception exc)
                        //  {
                        //      ExceptionHelper.HandleException(exc);
                        //  }

                }


                async void Handle_CredentialsCompleted(object sender, System.EventArgs e)
                {
                        var context = (SignupViewModel)this.BindingContext;
                        if (!context.Validate())
                        {
                                await context.ShowValidationErrors();
                        }
                        else
                        {
                                context.SignUpWithEmailCommand.Execute();
                        }
                }



    }
}
