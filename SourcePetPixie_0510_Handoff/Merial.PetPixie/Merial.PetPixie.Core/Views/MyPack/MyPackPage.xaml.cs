using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;
using Merial.PetPixie.Core.ViewModels;
using System.Collections.ObjectModel;
using Merial.PetPixie.Core.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Merial.PetPixie.Core.Shared;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.Helpers;

namespace Merial.PetPixie.Core.Views
{
    public partial class MyPackPage : MasterDetailPage
    {
        public ObservableCollection<PetModel> ImageItems { get; set; }
        public ObservableCollection<FeedItemViewModel> ImagePetPictures { get; set;}
        private MyPackViewModel vm;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (MyPackViewModel)this.BindingContext;

        }



        #region Load Pet Circle Images

        void ListItems_BindingContextChanged(object sender, System.EventArgs e)
        {
           


            var items = ((MyPackViewModel)listItems.BindingContext).Feeds;
            WrapItems.ItemsSource = items;// (DiscoverViewModel)listItems.BindingContext;
            WrapItems.BindingContext = items;// (DiscoverViewModel)listItems.BindingContext;
        }

        //Note that this is a temporary workaround to handle the issue of the binding not working
        void ListItems_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "ItemsSource"))
            {
                vm = (MyPackViewModel)this.BindingContext;
                WrapItems.Children.Clear();
                ImageItems = ((MyPackViewModel)listItems.BindingContext).UserPets;
                WrapItems.ItemsSource = ImageItems;
                WrapItems.BindingContext = ImageItems;

                if (ImageItems.Count > 0)
                {
                    LoadPetSummaries(ImageItems);

                }
            }
        }

        #endregion



        #region Load Pet Images

        void ListPetPictureGrid_BindingContextChanged(object sender, System.EventArgs e)
        {
            var items = ((MyPackViewModel)listItems.BindingContext).Feeds;
            WrapItems.ItemsSource = items;// (DiscoverViewModel)listItems.BindingContext;
            WrapItems.BindingContext = items;// (DiscoverViewModel)listItems.BindingContext;
        }

        //Note that this is a temporary workaround to handle the issue of the binding not working
        void ListPetPictureGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "ItemsSource"))
            {

                WrapPicsList.Children.Clear();
                WrapPicsGrid.Children.Clear();
                ImagePetPictures = ((MyPackViewModel)listPetPictureGrid.BindingContext).Feeds;

                LoadPetPictures(ImagePetPictures);


           
            }
        }


        //Note that this is a temporary workaround to handle the issue of the binding not working
        void LoadPetPictures(ObservableCollection<FeedItemViewModel> petPictures)
        {
            WrapPicsList.Children.Clear();
            WrapPicsGrid.Children.Clear();
            ImagePetPictures = ((MyPackViewModel)listPetPictureGrid.BindingContext).Feeds;

            if (petPictures.Count > 0)
            {
                foreach (FeedItemViewModel vm in petPictures)
                {
                    Image largePic = new Image() { Source = vm.ImageUrlLarge, VerticalOptions = LayoutOptions.Start, Aspect = Aspect.AspectFill, WidthRequest = 300 };
                    Image smallPic = new Image() { Source = vm.ImageUrlSmall, VerticalOptions = LayoutOptions.Start, Aspect = Aspect.AspectFill, WidthRequest = 100, HeightRequest = 100 };

                    smallPic.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            ((MyPackViewModel)BindingContext).ShowProfileCommand.Execute(vm.Feed);
                        }),
                        NumberOfTapsRequired = 1
                    });


                    largePic.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            ((MyPackViewModel)BindingContext).ShowProfileCommand.Execute(vm.Feed);
                        }),
                        NumberOfTapsRequired = 1
                    });

                    WrapPicsGrid.Children.Add(smallPic);
                    WrapPicsList.Children.Add(largePic);
                }

                WrapPicsList.IsVisible = false;
                WrapPicsGrid.IsVisible = true;

            }
            MessagingCenter.Send(this, "StopLoading");
        }

        public async void LoadPetSummaries(ObservableCollection<PetModel> petDetails)
        {

            await vm.LoadBreeds();

            foreach (PetModel pet in petDetails)
            {
                StackLayout stackPetDetails = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.Start,
                    WidthRequest = 70,
                    Spacing=0
                };


               var breedNames = vm.BreedsString(pet.BreedIds).Result;

               





                Label nameLabel = new Label() { Text = pet.Name, Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                Label reminderLabel = new Label() { Text = pet.NbrReminders.ToString(), Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 12, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                Label breedsLabel = new Label() { Text = breedNames, Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };

                Label ageLabel = new Label() { Text = PetAge(pet.Birthday), Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                CircleImage petImage = new CircleImage()
                {
                    BindingContext = pet,
                    Source = pet.ImageUrl,
                    WidthRequest = 45,
                    HeightRequest = 45,
                    BorderColor = Color.White,
                    BorderThickness = 1,
                    Aspect = Aspect.Fill,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start

                };




                petImage.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        var selectedPet = (PetModel)petImage.BindingContext;
                        ((MyPackViewModel)BindingContext).EditPetCommand.Execute(selectedPet);
                    }),
                    NumberOfTapsRequired = 1

                });


                stackPetDetails.Children.Add(petImage);
                stackPetDetails.Children.Add(nameLabel);
                stackPetDetails.Children.Add(reminderLabel);
                stackPetDetails.Children.Add(breedsLabel);
                stackPetDetails.Children.Add(ageLabel);
                WrapItems.Children.Add(stackPetDetails);
            }


        }




        #endregion



        //Note that this should be done as a converter
            public string PetAge(DateTime? petDOB)
        {


                var value = petDOB;
            if (value != null)
            {

                var timeAgo = DateTime.UtcNow - value.Value.Date;
                if (timeAgo.Days > 365)
                {
                    int years = (timeAgo.Days / 365);
                    if (timeAgo.Days % 365 != 0)
                        years += 1;
                    return $"{years} y" + " years old";
                }
                //if (timeAgo.Days > 30)
                //{
                //    int months = (timeAgo.Days / 30);
                //    if (timeAgo.Days % 31 != 0)
                //        months += 1;
                //    return $"{months} M";
                //}
                //if (timeAgo.Days > 0)
                //    return $"{timeAgo.Days} d";
                //if (timeAgo.Hours > 0)
                //    return $"{timeAgo.Hours} h";
                //if (timeAgo.Minutes > 0)
                //    return $"{timeAgo.Minutes} m";
                //if (timeAgo.Seconds > 5)
                //    return $"{timeAgo.Seconds} s";
                //if (timeAgo.Seconds <= 5)
                //    return "just now";
            }
                return string.Empty;


        }




        public MyPackPage()
        {
         
            InitializeComponent();

            SetNavigationProperties();

         //  bool breeds = await vm.LoadBreeds();

            listItems.BindingContextChanged += ListItems_BindingContextChanged;
            listItems.PropertyChanged += ListItems_PropertyChanged;


            listPetPictureGrid.BindingContextChanged += ListPetPictureGrid_BindingContextChanged;
            listPetPictureGrid.PropertyChanged += ListPetPictureGrid_PropertyChanged;

             
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


            FABHelper.SetFABProperties(navBar, scrollContent, ContentContainer, "Loading your pack...");

        }
         
        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("my pack");
                navBar.ShowNotificationCommand();
                navBar.ShowHamburgerCommand();
                navBar.ShowNotificationCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }

    }
}
