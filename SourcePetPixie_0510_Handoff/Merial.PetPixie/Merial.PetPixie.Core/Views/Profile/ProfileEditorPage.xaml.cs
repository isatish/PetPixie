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
    public partial class ProfileEditorPage : ContentPage
    {

        public ProfileEditorPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
        }
        public void SetNavigationBarProperties()
        {

                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("edit profile");
                navBar.ShowBackCommand();
                navBar.ShowAcceptCommand();


        }
    }
}
