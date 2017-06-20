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
using Merial.PetPixie.Core.Models.EventHandler;
using Plugin.Geolocator;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;
using MvvmCross.Platform;
using Acr.UserDialogs;


//using Plugin.ExternalMaps;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class VetMapPage : ContentPage
    {

        VetMapViewModel mapVM;

        public VetMapPage()
        {
            InitializeComponent();
            Mvx.Resolve<IUserDialogs>().ShowLoading("Loading nearby vets");

             mapVM = (VetMapViewModel)this.BindingContext;

            MessagingCenter.Subscribe<VetMapViewModel, List<KVet>>(this, "Initialized", (sender, results) =>
            {
                List<KVet> searchResults = results;

                SetLocation(searchResults);
                ScrollToVets(results.Count);
            });

            SetNavigationBarProperties();
            EntrySearch.Completed += EntrySearch_Completed;
            //  SetLocation();

            //      CrossExternalMaps.Current.NavigateTo("Xamarin", "394 pacific ave.", "San Francisco", "CA", "94111", "USA", "USA");



        }


        public async void ScrollToVets(int totalVets)
        {
            scrollVets.ScrollToAsync(LabelHeader, ScrollToPosition.Start, true);

            int listHeight = 60 * totalVets;
            listItems.HeightRequest = listHeight;
            //  scrollVets.ScrollToAsync(0, 300, true);//ScrollToAsync(0,50,true);
            //   stackVets.Sc.  Y = stackVets.Y - 50;

            stackVets.HeightRequest = 230 + listHeight;
        }

        public async void SetLocation(List<KVet> searchResults)
        {
          

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var currPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Xamarin.Forms.Maps.Position(currPosition.Latitude, currPosition.Longitude);

            /*


            //var position = new Position(37.79762, -122.40181);
            MapVets.MoveToRegion(new MapSpan(position, 10, 10));
            //MapVets.Pins.Add(new Pin
            //{
            //    Label = "Current Location",
            //    Position = position
            //});

            var pin = new CustomPin
            {
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position, // new Position(lat, lng),
                    Label = "vet.Name",
                    Address = "vet.Address",
                },
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };

        //    MapVets.CustomPins = new List<CustomPin> { pin };
        //    MapVets.Pins.Add(pin.Pin);

        //    MapVets.CustomPins.Add(pin);
        //   MapVets.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(currPosition.Latitude, currPosition.Longitude), Distance.FromMiles(1.0)));



            //    await mapVM.SearchByLatLong(currPosition.Latitude, currPosition.Longitude, true);
            if (searchResults != null)
            {
                foreach (KVet vet in searchResults)

                {
                    var name = vet.Name;
                    double lng = vet.Geoloc.FirstOrDefault();
                    double lat = vet.Geoloc.LastOrDefault();

                   // var vetPosition = new Xamarin.Forms.Maps.Position(lat, lng);

                    //var position = new Position(37.79762, -122.40181);
                    //   MapVets.MoveToRegion(new MapSpan(vetPosition, 0.01, 0.01));
                    //MapVets.Pins.Add(new Pin
                    //{
                    //    Label = name,
                    //    Position = vetPosition
                    //});

                    //MapVets.CustomPins = new List<CustomPin>();
                    //MapVets.CustomPins.Add(new CustomPin()
                    //{
                    //    Id = vet.Id,
                    //    Pin = new Pin
                    //    {
                    //        Label = name,
                    //        Position = vetPosition
                    //    },
                    //    Url = ""
                    //});

















                }

            }




            */



            List<CustomPin> customPins = new List<CustomPin>();

           


            var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest =600,// App.ScreenWidth,
                HeightRequest = 600// App.ScreenHeight
            };



            if (searchResults != null)
            {
                foreach (KVet vet in searchResults)

                {
                    var name = vet.Name;
                    double lng = vet.Geoloc.FirstOrDefault();
                    double lat = vet.Geoloc.LastOrDefault();

                    var searchPin = new CustomPin
                    {
                        Pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(lat, lng),
                            Label = vet.Name,
                            Address = vet.Address
                        },
                        Id = vet.Id,
                        Url = vet.Website
                    };



                    customPins.Add(searchPin);
                    customMap.Pins.Add(searchPin.Pin);

                }
            }
                    // var vetPosition = new Xamarin.Forms.Maps.Position(lat, lng);

                    //var position = new Position(37.79762, -122.40181);
                    //   MapVets.MoveToRegion(new MapSpan(vetPosition, 0.01, 0.01));
                    //MapVets.Pins.Add(new Pin
                    //{
                    //    Label = name,
                    //    Position = vetPosition
                    //});

                    //MapVets.CustomPins = new List<CustomPin>();
                    //MapVets.CustomPins.Add(new CustomPin()
                    //{
                    //    Id = vet.Id,
                    //    Pin = new Pin
                    //    {
                    //        Label = name,
                    //        Position = vetPosition
                    //    },
                    //    Url = ""
                    //});








            customMap.CustomPins = customPins;// new List<CustomPin> { pin };
           // customMap.Pins.Add(pin.Pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(currPosition.Latitude, currPosition.Longitude), Distance.FromMiles(1.0)));

            ContentMap.Content = customMap;
//            Content = customMap;

            Mvx.Resolve<IUserDialogs>().HideLoading();

        }


        public void SetNavigationBarProperties()
        {
            // try
            // {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("my vet");
            navBar.ShowBackCommand();

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {
            VetMapViewModel mapVM = (VetMapViewModel)this.BindingContext;


            string expression = EntrySearch.Text;

            if (!string.IsNullOrWhiteSpace(expression))
            {
                //           return new List<SearchResult>();
                var vetsFound = SearchBoxOnCustomSearch(expression).Result;
            }








        }






        public async void ShowVetsOnMap()
        {
            var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest = 300,// App.ScreenWidth,
                HeightRequest = 400//App.ScreenHeight
            };

            var pin = new CustomPin
            {
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(37.79752, -122.40183),
                    Label = "Xamarin San Francisco Office",
                    Address = "394 Pacific Ave, San Francisco CA"
                },
                Id = "Xamarin",
                Url = ""//"http://xamarin.com/about/"
            };

            MapVets.CustomPins = new List<CustomPin> { pin };
            MapVets.Pins.Add(pin.Pin);
         //   customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));

        //    MapVets = customMap;
        }





                private async Task<List<KVet>> SearchBoxOnCustomSearch(string expression)
                {

                        VetMapViewModel mapVM = (VetMapViewModel)this.BindingContext;

                        if (string.IsNullOrWhiteSpace(expression))
                        {
                                return new List<KVet>();
                        }
                        //if (expression.Length == 5)
                        //{
                        //        //                  var searchVetResults = (await mapVM.SearchByLatLong(expression));
                        //        var searchVetResults = (await mapVM..SearchVetByName(expression));
                        //}
                        //else
                        //{
                        //        var searchVetResults = (await mapVM.SearchVetByName(expression));
                        //}

                        var searchResults = (await mapVM.SearchVetByName(expression)).Select(v => new VetSearchResult(v, "vet1.png")).Cast<KVet>().ToList();
                        return searchResults;
                }



                private string zipCodeToLatLong(string zipCode)
                {
                        string latLong = "";

                        var latLongs = "";

                        return latLong;


                }


    }
}