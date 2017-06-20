using Xamarin.Forms;
using Merial.PetPixie.Core.Effects;
using System;
using FAB.Forms;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.Helpers;

namespace Merial.PetPixie.Core.Views
{
    public partial class SettingsPage : MasterDetailPage
    {

        public SettingsPage()
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
         //   FABHelper.SetFABProperties(navBar, MainContentsStack, ContentContainer);

        }

        public void SetNavigationBarProperties()
        {
            try
            {
                
                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("Settings");
                navBar.ShowHamburgerCommand();
            }
            catch (Exception exc)
            {
                //ExceptionHelper.HandleException(exc);
            }

        }

    }
}
