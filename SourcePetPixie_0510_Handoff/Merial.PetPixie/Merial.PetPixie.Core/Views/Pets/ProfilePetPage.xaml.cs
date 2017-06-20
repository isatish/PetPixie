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
using Merial.PetPixie.Core.Models.Enums;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class ProfilePetPage : ContentPage
    {

        public ProfilePetPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
            MessagingCenter.Subscribe<ProfilePetViewModel, EntityMode>(this, "EntityMode", (sender, entityMode) =>
             {
                 if (entityMode == EntityMode.Edit)
                 {
                     navBar.ShowHeaderText("edit pet");
                 }
                 else
                 {
                     navBar.ShowHeaderText("add a pet");
                 }
             });

                           
                       
                        MessagingCenter.Subscribe<ProfilePetViewModel>(this, "SavingPet", (sender) =>
             {

                      

                var loader = new Shared.Loader();

                loader.SetLoadingMessage("Saving your Pet Profile changes.");
                 stackLoader.Children.Clear();
                 stackLoader.Children.Add(loader);
                        });
                                                                                      

            }

        public void SetNavigationBarProperties()
        {
           // try
           // {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("Edit pet");
                navBar.ShowBackCommand();
                navBar.ShowAcceptCommand();

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
