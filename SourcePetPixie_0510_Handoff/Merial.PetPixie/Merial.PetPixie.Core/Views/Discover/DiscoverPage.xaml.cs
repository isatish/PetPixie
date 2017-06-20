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
using Merial.PetPixie.Core.Shared;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class DiscoverPage : MasterDetailPage
    {

        int totalRows = 0;
        private readonly DataTemplate templateOne;
        private readonly DataTemplate templateTwo;
        public ObservableCollection<DiscoverItemViewModel> ImageItems { get; set; }
 


        public DiscoverPage()
        {
            InitializeComponent();
            SetNavigationProperties();
        }


        public void SetNavigationProperties()
        {
            Master = new LeftNavBar() { Title = "Nav Title" };
            IsPresented = false;

            SetNavigationBarProperties();
            MessagingCenter.Subscribe<ViewModelBase>(this, "ShowNavigation", (sender) =>
            {
                IsPresented = true;
            });
            FABHelper.SetFABProperties(navBar, ScrollContent, ContentContainer,  "Discovering images....");

        }
        public void SetNavigationBarProperties()
        {
           
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowSearchCommand();
            navBar.ShowHamburgerCommand();
            navBar.ShowHeaderText("discover");

        }
    }
}
