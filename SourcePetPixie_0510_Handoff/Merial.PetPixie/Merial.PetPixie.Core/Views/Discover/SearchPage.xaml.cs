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
    public partial class SearchPage : ContentPage
    {

        int totalRows = 0;
        private readonly DataTemplate templateOne;
        private readonly DataTemplate templateTwo;
        public ObservableCollection<DiscoverItemViewModel> ImageItems { get; set; }



        public SearchPage()
        {
            InitializeComponent();

         //   listItems.BindingContextChanged += ListItems_BindingContextChanged;
         //   listItems.PropertyChanged += ListItems_PropertyChanged;
            SetNavigationBarProperties();
        }


        public void SetNavigationBarProperties()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowSearchCommand();
            navBar.ShowBackCommand();
            navBar.ShowHeaderText("Search");
            MessagingCenter.Subscribe<DiscoverSearchViewModel, string>(this, "SearchTag", (sender, tagValue) =>
            {
                navBar.ShowHeaderText(tagValue.ToLower());
            });

        }
    }
}
