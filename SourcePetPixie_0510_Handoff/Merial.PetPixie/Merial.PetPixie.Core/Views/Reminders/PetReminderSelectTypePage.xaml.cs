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

namespace Merial.PetPixie.Core.Views.Reminder
{
    public partial class PetReminderSelectTypePage : ContentPage
    {

        public PetReminderSelectTypePage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
            //     ((MyVetsViewModel)this.BindingContext).Init(false);
            //EntrySearch.Completed += EntrySearch_Completed;

            //      CrossExternalMaps.Current.NavigateTo("Xamarin", "394 pacific ave.", "San Francisco", "CA", "94111", "USA", "USA");
        }
        public void SetNavigationBarProperties()
        {
            // try
            // {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("reminder");
            navBar.ShowBackCommand();
            navBar.ShowAddReminderCommand();

            //   var title = AppResources.AboutTitle;
            //   navBar.ShowHeaderText(title);
            //   navBar.ShowBack();
            // }
            //  catch (Exception exc)
            //  {
            //      ExceptionHelper.HandleException(exc);
            //  }

            /*
            if (Settings.CurrentUserProfile != null)
            {

                var Vets = new ObservableCollection<VetModel>(Settings.CurrentUserProfile.Vets.Select(VetModel.CreateFrom));
                //if ((Vets == null || !Vets.Any()) && _redirect)
                //{
                //    _redirect = false;
                //    ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters { SelectedVet = null });
                //}

                foreach (var vet in Vets)

                {
                    var name = vet.Name;
                    double lng = vet.GeoLoc.FirstOrDefault();
                    double lat = vet.GeoLoc.LastOrDefault();

                    var position = new Position(lat, lng);

                }
            }

*/
        }

        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
