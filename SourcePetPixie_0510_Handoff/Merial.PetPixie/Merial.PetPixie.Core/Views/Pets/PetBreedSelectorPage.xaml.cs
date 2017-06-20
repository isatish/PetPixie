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
using Merial.PetPixie.Core.Models.Kinvey;

//using Plugin.ExternalMaps;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class PetBreedSelectorPage : ContentPage
    {

        public PetBreedSelectorPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();

            MessagingCenter.Subscribe<PetBreedSelectorViewModel>(this, "BreedSelected", (sender) =>
            {
                LoadBreeds();
            });

        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            LoadBreeds();
        }
       
        public void SetNavigationBarProperties()
        {
            // try
            // {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("breed");
            navBar.ShowBackCommand();

        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }

        void LoadBreeds()
        {
            StackBreeds.Children.Clear();
            PetBreedSelectorViewModel vm = (PetBreedSelectorViewModel)this.BindingContext;
            if (vm != null)
            {
                foreach (var breed in vm.SelectedBreeds)
                {
                    StackLayout stackBreed = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.FromHex("#e23229") };
                    Button buttonRemoveBreed = new Button() { Image = "IconClose.png", HeightRequest=15, WidthRequest=15 };
                    buttonRemoveBreed.BindingContext = breed;
                    buttonRemoveBreed.Clicked += ButtonBreedName_Clicked;




                    Button buttonBreedName = new Button() { Text = breed.Name, FontSize = 10, HeightRequest = 30, Margin=5, BackgroundColor = Color.FromHex("#e23229"), TextColor = Color.White };
                    buttonBreedName.BindingContext = breed;
                    buttonBreedName.Clicked += ButtonBreedName_Clicked;


                    stackBreed.Children.Add(buttonRemoveBreed);
                    stackBreed.Children.Add(buttonBreedName);
                 Frame frameBreed = new Frame() { Padding = 2, BackgroundColor = Color.FromHex("#e23229") };
                 frameBreed.Content = stackBreed;
                 StackBreeds.Children.Add(frameBreed);


                }
            }
        }

        void ButtonBreedName_Clicked(object sender, System.EventArgs e)
        {
            Button breedButton = (Button)sender;

            KBreed currentBreed = (KBreed)(breedButton.BindingContext);

            ProfilePetViewModel.SelectedBreeds.RemoveAll(b => b.Id == currentBreed.Id);
            LoadBreeds();

        }
    }
}
