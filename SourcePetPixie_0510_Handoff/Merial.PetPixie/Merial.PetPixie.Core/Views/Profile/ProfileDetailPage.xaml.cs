using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;
using Merial.PetPixie.Core.ViewModels;
using System.Collections.ObjectModel;
using Merial.PetPixie.Core.Models;
using ImageCircle.Forms.Plugin.Abstractions;

namespace Merial.PetPixie.Core.Views
{
    public partial class ProfileDetailPage : ContentPage
    {

        public ObservableCollection<PetModel> ImageItems { get; set; }
        public ObservableCollection<PetModel> ImagePetPictures { get; set; }



        #region Load Pet Circle Images

        void ListItems_BindingContextChanged(object sender, System.EventArgs e)
        {

            var profile = ((ProfileDetailViewModel)listItems.BindingContext).Profile;
            if (profile != null)
            {
                var items = profile.Pets;
                WrapItems.ItemsSource = items;// (DiscoverViewModel)listItems.BindingContext;
                WrapItems.BindingContext = items;// (DiscoverViewModel)listItems.BindingContext;
            }
        }

        //Note that this is a temporary workaround to handle the issue of the binding not working
        void ListItems_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "ItemsSource"))
            {

                WrapItems.Children.Clear();
                var profile = ((ProfileDetailViewModel)listItems.BindingContext).Profile;
                if (profile != null)
                {
                    //ds  ImageItems = ((MyPackViewModel)listItems.BindingContext).UserPets;
                    ImageItems = profile.Pets;
                    WrapItems.ItemsSource = ImageItems;
                    WrapItems.BindingContext = ImageItems;

                    if (ImageItems.Count > 0)
                    {
                        foreach (PetModel vm in ImageItems)
                        {
                            StackLayout stackPetDetails = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                VerticalOptions = LayoutOptions.Start,
                                WidthRequest = 70
                            };

                            Label nameLabel = new Label() { Text = vm.Name, TextColor = Color.White, FontSize = 10, HeightRequest = 12, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                            Label reminderLabel = new Label() { Text = vm.NbrReminders.ToString(), TextColor = Color.White, FontSize = 10, HeightRequest = 12, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                            Label ageLabel = new Label() { Text = PetAge(vm.Birthday), TextColor = Color.White, FontSize = 10, HeightRequest = 12, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                            CircleImage petImage = new CircleImage()
                            {
                                Source = vm.ImageUrl,
                                WidthRequest = 50,
                                HeightRequest = 50,
                                BorderColor = Color.White,
                                BorderThickness = 1,
                                Aspect = Aspect.Fill,
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.Start

                            };

                            stackPetDetails.Children.Add(petImage);
                            stackPetDetails.Children.Add(nameLabel);
                            stackPetDetails.Children.Add(reminderLabel);
                            stackPetDetails.Children.Add(ageLabel);
                            WrapItems.Children.Add(stackPetDetails);
                        }

                    }

                }
                }
        }

        #endregion



        #region Load Pet Images

        void ListPetPictureGrid_BindingContextChanged(object sender, System.EventArgs e)
        {
            var profile = ((ProfileDetailViewModel)listItems.BindingContext).Profile;
            if (profile != null)
            {

                var items = profile.Pets;
                WrapItems.ItemsSource = items;// (DiscoverViewModel)listItems.BindingContext;
                WrapItems.BindingContext = items;// (DiscoverViewModel)listItems.BindingContext;
            }
        }

        //Note that this is a temporary workaround to handle the issue of the binding not working
        void ListPetPictureGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "ItemsSource"))
            {

                WrapPicsList.Children.Clear();
                WrapPicsGrid.Children.Clear();


                var profile = ((ProfileDetailViewModel)listItems.BindingContext).Profile;
                if (profile != null)
                {


                    ImagePetPictures = profile.Pets;

                    //WrapPicsList.ItemsSource = items;
                    //WrapPicsList.BindingContext = items;

                    if (ImagePetPictures.Count > 0)
                    {
                        foreach (PetModel vm in ImagePetPictures)
                        {
                            StackLayout stackPetDetails = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                WidthRequest = 70,
                                HeightRequest = 120
                            };

                            //       Image largePic = new Image() { Source = vm.ImageUrlLarge, VerticalOptions=LayoutOptions.Start, WidthRequest = 300, HeightRequest = 300 };

                            //       Image smallPic = new Image() { Source = vm.ImageUrlSmall, VerticalOptions = LayoutOptions.Start, WidthRequest = 100, HeightRequest = 100 };


                            //ds     WrapPicsGrid.Children.Add(smallPic);
                            //ds     WrapPicsList.Children.Add(largePic);
                        }

                        WrapPicsList.IsVisible = false;
                        WrapPicsGrid.IsVisible = true;

                    }
                }
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




        public ProfileDetailPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
            //SetFABProperties();
            listItems.BindingContextChanged += ListItems_BindingContextChanged;
            listItems.PropertyChanged += ListItems_PropertyChanged;


            listPetPictureGrid.BindingContextChanged += ListPetPictureGrid_BindingContextChanged;
            listPetPictureGrid.PropertyChanged += ListPetPictureGrid_PropertyChanged;

             
        }
        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("profile details");
                navBar.ShowNotificationCommand();
                navBar.ShowBackCommand();
                navBar.ShowNotificationCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }



    }
}
