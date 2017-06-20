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
using Merial.PetPixie.Core.Converters;
using System.Collections.Generic;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class FindFriendsTabsPage : MasterDetailPage
    {

        int totalRows = 0;
        private readonly DataTemplate templateOne;
        private readonly DataTemplate templateTwo;
        public ObservableCollection<DiscoverItemViewModel> ImageItems { get; set; }
 


        public FindFriendsTabsPage()
        {
            InitializeComponent();
            SetNavigationProperties();
        
            //This worked!
  //      testLabel.SetBinding(Label.TextProperty, new Binding("TestConv", BindingMode.Default, new StringReverseConverter()));



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
            FABHelper.SetFABProperties(navBar, stackCarousel, ContentContainer, "Loading your friends...");

        }
        public void SetNavigationBarProperties()
        {
           
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowSearchCommand();
            navBar.ShowBackCommand();
            navBar.ShowHeaderText("find friends");

            this.MyCarouselView.ItemsSource = new List<int> { 1, 2};
            this.MyCarouselView.Position = 0;
            this.MyCarouselView.BackgroundColor = Color.Transparent;

        }
    }
}
