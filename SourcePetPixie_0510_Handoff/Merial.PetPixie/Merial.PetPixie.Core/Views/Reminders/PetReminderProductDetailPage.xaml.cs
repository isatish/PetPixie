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
    public partial class PetReminderProductDetailPage : MasterDetailPage
    {

        public PetReminderProductDetailPage()
        {
            InitializeComponent();
            SetNavigationProperties();
        }

        public void SetNavigationProperties()
        {
            SetNavigationBarProperties();
       //     FABHelper.SetFABProperties(navBar, StackDeails, ContentContainer);


            Master = new LeftNavBar() { Title = "Feed" };
            IsPresented = false;

            MessagingCenter.Subscribe<ViewModelBase>(this, "ShowNavigation", (sender) =>
                {
                    IsPresented = true;
                });


        }

                void Handle_ShowDatePickerClicked(object sender, System.EventArgs e)
                {
                         ReminderStartDate.IsVisible = true;
                }

        public void SetNavigationBarProperties()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("reminders");
            navBar.ShowBackCommand();
            navBar.ShowAddReminderCommand();
                        navBar.ShowDeleteReminderCommand();

           
        }




     

        //public async void LoadPetSummaries(ObservableCollection<PetModel> petDetails)
        //{

        //    await vm.LoadBreeds();

        //    foreach (PetModel pet in petDetails)
        //    {
        //        StackLayout stackPetDetails = new StackLayout()
        //        {
        //            Orientation = StackOrientation.Vertical,
        //            VerticalOptions = LayoutOptions.Start,
        //            WidthRequest = 70,
        //            Spacing = 0
        //        };


        //        var breedNames = vm.BreedsString(pet.BreedIds).Result;







        //        Label nameLabel = new Label() { Text = pet.Name, Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
        //        Label reminderLabel = new Label() { Text = pet.NbrReminders.ToString(), Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 12, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
        //        Label breedsLabel = new Label() { Text = breedNames, Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };

        //        Label ageLabel = new Label() { Text = PetAge(pet.Birthday), Margin = new Thickness(0), TextColor = Color.White, FontSize = 10, HeightRequest = 10, WidthRequest = 100, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
        //        CircleImage petImage = new CircleImage()
        //        {
        //            BindingContext = pet,
        //            Source = pet.ImageUrl,
        //            WidthRequest = 45,
        //            HeightRequest = 45,
        //            BorderColor = Color.White,
        //            BorderThickness = 1,
        //            Aspect = Aspect.Fill,
        //            VerticalOptions = LayoutOptions.Start,
        //            HorizontalOptions = LayoutOptions.Start

        //        };




        //        petImage.GestureRecognizers.Add(new TapGestureRecognizer
        //        {
        //            Command = new Command(() =>
        //            {
        //                var selectedPet = (PetModel)petImage.BindingContext;
        //                ((MyPackViewModel)BindingContext).EditPetCommand.Execute(selectedPet);
        //            }),
        //            NumberOfTapsRequired = 1

        //        });


        //        stackPetDetails.Children.Add(petImage);
        //        stackPetDetails.Children.Add(nameLabel);
        //        stackPetDetails.Children.Add(reminderLabel);
        //        stackPetDetails.Children.Add(breedsLabel);
        //        stackPetDetails.Children.Add(ageLabel);
        //        WrapItems.Children.Add(stackPetDetails);
        //    }


        //}




      //  #endregion


        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
