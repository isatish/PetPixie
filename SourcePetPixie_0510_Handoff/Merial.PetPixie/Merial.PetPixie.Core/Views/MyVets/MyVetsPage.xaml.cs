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
using Plugin.Geolocator;
using System.Collections.Generic;

//using Plugin.ExternalMaps;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class MyVetsPage : MasterDetailPage
    {

        public MyVetsPage()
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
            FABHelper.SetFABProperties(navBar, GridContent, ContentContainer, "Loading your vets....");

        }

        public void SetNavigationBarProperties()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("my vets");
            navBar.ShowHamburgerCommand();

           

        }



        public async void SetCurrentMapLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var currPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Xamarin.Forms.Maps.Position(currPosition.Latitude, currPosition.Longitude);




            var currPos = new Position(37.79762, -122.40181);



            //position = new Position(25.9476328, -80.1524747);
            //mapVets.MoveToRegion(new MapSpan(position, 0.01, 0.01));
            //mapVets.Pins.Add(new Pin
            //{
            //    Label = "Current Location",
            //    Position = position
            //});


            var pin = new CustomPin
            {
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = currPos,  //new Position(25.9476328, -80.1524747),
                    Label = "Xamarin San Francisco Office",
                    Address = "394 Pacific Ave, San Francisco CA"
                },
                Id = "Xamarin",
                Url = "" // "http://xamarin.com/about/"
            };

            mapVets.CustomPins = new List<CustomPin> { pin };
            mapVets.Pins.Add(pin.Pin);

            mapVets.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));

        //    mapVets.MoveToRegion(new MapSpan(currPos, 0.01, 0.01));



           //  var mapVM = (MyVetsViewModel)this.BindingContext;

            /*
          var results = await mapVM.SearchByLatLong(currPosition.Latitude, currPosition.Longitude, true);

          if (string.IsNullOrWhiteSpace(expression))
          {
              return new List<KVet>();
          }
          var searchVetResults = (await mapVM.SearchVetByName(expression));


          var searchResults = (await mapVM.SearchVetByName(expression)).Select(v => new VetSearchResult(v, "vet1.png")).Cast<KVet>().ToList();
          return searchResults;


          */

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
