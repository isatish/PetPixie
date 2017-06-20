using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class NotificationSettingsPage : ContentPage
    {
        public NotificationSettingsPage()
        {
            InitializeComponent();
            SetNavigationBarProperties();

        }
        public void SetNavigationBarProperties()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("notifications");
                navBar.ShowBackCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }




    }
}
