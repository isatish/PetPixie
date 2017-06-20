using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.Helpers;
using Xamarin.Forms;

namespace Merial.PetPixie.Core
{
	public partial class SettingsPage : ContentPage
	{
        private readonly INavigation _navigation;

		public SettingsPage ()
		{
			InitializeComponent ();
           // _navigation = navigation;

            BindingContext = new SettingsViewModel();
		}


        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var setting = settingsListView.SelectedItem as Sample;

                if (setting != null)
                {
                    if (setting.PageType == typeof(RootPage))
                    {
                        await DisplayAlert("Hey", string.Format("You are already here, on sample {0}.", setting.Name), "OK");
                    }
                    else {
                        await Navigation.PushModalAsync(new NavigationPage(new AboutUsPage()));
                        //  await setting.NavigateToSample(Navigation.PushModalAsync(new AboutUsPage()));
                    }

                    settingsListView.SelectedItem = null;
                }

            }
            catch (Exception exc)
            {
                ExceptionHelper.HandleException(exc);
            }
        }


    
        public void OnItemTapped (object sender, EventArgs e) {

			 
		}
	}
}

