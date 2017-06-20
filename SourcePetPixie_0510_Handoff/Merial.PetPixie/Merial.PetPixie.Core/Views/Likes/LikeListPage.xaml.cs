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

namespace Merial.PetPixie.Core.Views
{
    public partial class LikeListPage : ContentPage
    {

        public LikeListPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();

        }
        public void SetNavigationBarProperties()
        {
            // try
            // {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("likes");
            navBar.ShowBackCommand();

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
