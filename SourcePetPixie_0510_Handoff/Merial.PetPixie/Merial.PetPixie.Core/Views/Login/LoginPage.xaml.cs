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
using System;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class LoginPage : ContentPage, IFacebookService
    {
     

 


        public LoginPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();

//            App.PostSuccessFacebookAction = async token =>
//{
//    //you can use this token to authenticate to the server here
//    //call your FacebookLoginService.LoginToServer(token)
                        //    //I'll just navigate to a screen that displays the token:

//    //Do the actual login here

//    var fbToken = token.ToString();



//};
        }

       async  void Handle_CredentialsCompleted(object sender, System.EventArgs e)
        {
                var context =  (LoginViewModel)this.BindingContext;
                if (!context.Validate())
                {
                       await context.ShowValidationErrors();
                }
                else
                {
                        context.LoginCommand.Execute();
                }
        }

        public event EventHandler<FacebookUserModel> DataResponse;

        public void GetFacebookData()
        {
            throw new NotImplementedException();
        }

        public void SetNavigationBarProperties()
        {
           // try
           // {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("Login");

           }
    }
}
