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

namespace Merial.PetPixie.Core.Views
{
    public partial class MyVetInfoPage : ContentPage
    {

        public MyVetInfoPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
      

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var geoLoc = ((MyVetInfoViewModel)this.BindingContext).SelectedVet.Geoloc;
            var displayName = ((MyVetInfoViewModel)this.BindingContext).SelectedVet.Name;

            double geoLong = geoLoc.FirstOrDefault();
            double geoLat = geoLoc.LastOrDefault();

            var position = new Position(geoLat, geoLong);
            MapVets.Pins.Clear();
            Pin mapPin = new Pin();
            MapVets.Pins.Add(new Pin() { Position = position, Label=displayName, Type=PinType.Place });
            MapVets.MoveToRegion(new MapSpan(position, -10, -10));

        }

        public void SetNavigationBarProperties()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("vet details");
            navBar.ShowBackCommand();

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
