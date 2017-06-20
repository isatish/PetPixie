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
using Xamarin.Forms.Maps;

//using Plugin.ExternalMaps;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views.Reminder
{
    public partial class PetReminderSelectProductPage : ContentPage
    {

        public PetReminderSelectProductPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
        }
        public void SetNavigationBarProperties()
        {
 
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("reminder");
            navBar.ShowBackCommand();
            navBar.ShowAddReminderCommand();
        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
