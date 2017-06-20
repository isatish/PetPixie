using System;
using Xamarin.Forms;

namespace Merial.PetPixie.Core
{
	public partial class SettingsView : ContentPage
	{
		private readonly INavigation _navigation;

		public SettingsView (INavigation navigation)
		{
			InitializeComponent ();

			_navigation = navigation;

			BindingContext = new SamplesViewModel(navigation);
		}

		public async void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
            
			var sample = sampleListView.SelectedItem as Sample;

			if (sample != null) {
				if (sample.PageType == typeof(RootPage)) {
					await DisplayAlert ("Hey", string.Format ("You are already here, on sample {0}.", sample.Name), "OK");
				} else {
					await sample.NavigateToSample (_navigation);
				}

				sampleListView.SelectedItem = null;
			}
		}
			
        private async void OnCloseButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }
	}
}