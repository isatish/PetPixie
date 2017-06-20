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

namespace Merial.PetPixie.Core.Views.Reminder
{
    public partial class PetReminderListPage : MasterDetailPage
    {

        public PetReminderListPage()
        {
            InitializeComponent();
            SetNavigationProperties();
        }

        public void SetNavigationProperties()
        {
            SetNavigationBarProperties();
            FABHelper.SetFABProperties(navBar, listItems, ContentContainer, "Loading Reminders...");


            Master = new LeftNavBar() { Title = "Feed" };
            IsPresented = false;

            MessagingCenter.Subscribe<ViewModelBase>(this, "ShowNavigation", (sender) =>
                {
                    IsPresented = true;
                });


        }

        public void SetNavigationBarProperties()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("reminders");
            navBar.ShowBackCommand();
            // navBar.ShowAddReminderCommand();

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
