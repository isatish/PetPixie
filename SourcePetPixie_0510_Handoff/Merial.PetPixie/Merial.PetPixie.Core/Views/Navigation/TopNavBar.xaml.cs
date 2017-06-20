using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class TopNavBar : ContentView
    {


        public TopNavBar()
        {
            InitializeComponent();
            //navFriends.GestureRecognizers.Add
            HideNotificationCommand();
            HideFindFriendsCommand();
            HideBackCommand();
            HideHamburgerCommand();
            HideAcceptCommand();
            HideAddReminderCommand();
            HideEditReminderCommand();
           HideDeleteReminderCommand();
            HideAcceptCropCommand();
            HideAcceptShareCommand();
            HideAcceptFunCommand();
            HideSearchCommand();


        }

        public void ShowHeaderText(string headerText)
        {
            
            labelHeader.Text = headerText;
        }


        public void ShowNotificationCommand()
        {
            ButtonNotificationsCommand.IsVisible = true;
        }

        public void HideNotificationCommand()
        {
            ButtonNotificationsCommand.IsVisible = false;
        }



        #region  FindFriends Command Visibility

        public void ShowFindFriendsCommand()
        {
            ButtonFindFriendsCommand.IsVisible = true;
        }

        public void HideFindFriendsCommand()
        {
            ButtonFindFriendsCommand.IsVisible = false;
        }

        #endregion


        #region  Accept Command Visibility


        public void ShowAcceptCommand()
        {
            ButtonAcceptCommand.IsVisible = true;
        }

        public void HideAcceptCommand()
        {
            ButtonAcceptCommand.IsVisible = false;
        }

        #endregion

        #region  Show AcceptCrop Command Visibility


        public void ShowAcceptCropCommand()
        {
            ButtonAcceptCropCommand.IsVisible = true;
        }

        public void HideAcceptCropCommand()
        {
            ButtonAcceptCropCommand.IsVisible = false;
        }




        #endregion

        #region  Show AcceptShare Command Visibility


        public void ShowAcceptShareCommand()
        {
            ButtonAcceptShareCommand.IsVisible = true;
        }

        public void HideAcceptShareCommand()
        {
            ButtonAcceptShareCommand.IsVisible = false;
        }

          #endregion


        #region Show ShareFun Command Visibility

        public void ShowAcceptFunCommand()
        {
            ButtonAcceptFunCommand.IsVisible = true;
        }

        public void HideAcceptFunCommand()
        {
            ButtonAcceptFunCommand.IsVisible = false;
        }

        #endregion

        #region Show Search Command Visibility

        public void ShowSearchCommand()
        {
            ButtonSearchCommand.IsVisible = true;
        }

        public void HideSearchCommand()
        {
            ButtonSearchCommand.IsVisible = false;
        }

        #endregion

        #region  Show tReminder Command Visibility

        public void HideAddReminderCommand()
        {
            ButtonAddReminderCommand.IsVisible = false;
        }

        public void ShowAddReminderCommand()
        {
            ButtonAddReminderCommand.IsVisible = true;
        }

        #endregion


        #region  Show EditReminder Command Visibility

        public void HideEditReminderCommand()
        {
        	ButtonEditReminderCommand.IsVisible = false;
        }

        public void ShowEditReminderCommand()
        {
        	ButtonEditReminderCommand.IsVisible = true;
        }

        #endregion


        #region  Show DeleteReminder Command Visibility

        public void HideDeleteReminderCommand()
        {
        	ButtonDeleteReminderCommand.IsVisible = false;
        }

        public void ShowDeleteReminderCommand()
        {
        	ButtonDeleteReminderCommand.IsVisible = true;
        }

        #endregion





        #region  BackCommand Visibility


        public void ShowBackCommand()
        {
            ButtonBack.IsVisible = true;
        }

        public void HideBackCommand()
        {
            ButtonBack.IsVisible = false;
        }

        #endregion

        #region  HamburgerCommand Visibility


        public void ShowHamburgerCommand()
        {
            ButtonHamburger.IsVisible = true;
        }

        public void HideHamburgerCommand()
        {
            ButtonHamburger.IsVisible = false;
        }


        #endregion

        void Handle_Tapped(object sender, System.EventArgs e)
        {
        //    NavigationPage _navPage = new NavigationPage(new Option3Page());

            // Assign your NavigationPage to your MasterDetail
        //    Detail = _navPage;

            // When you want to move
          //  _navPage.PushAsync(new MyNewPage());
        }
    }
}

