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
using FAB.Forms;
using System;
using Plugin.Media;
using MvvmCross.Platform;
using Acr.UserDialogs;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class FeedPage : MasterDetailPage
    {




        public FeedPage()
        {
            InitializeComponent();

                        if (Settings.ShowGetStarted)
                        {
                                ShowTourCarousel();
                        }
                        else
                        {

                                SetNavigationProperties();
                        }
     

                     MessagingCenter.Subscribe<GetStartedTour>(this, "HideTour", (sender) =>
                     {
                             HideTourCarousel();

                             SetNavigationProperties();
                      });

 


        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

                protected override void OnAppearing()
                {
                        var vm = (FeedViewModel)this.BindingContext;

                        if (Settings.ShowGetStarted)
                        {
                                ShowTourCarousel();

                                if (vm.IsLoading == false)
                                {
                                        //dsloading  Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed");
                                }
                        }

                   
        }


                public void ShowTourCarousel()
                {
                              NavigationPage.SetHasNavigationBar(this, false);
                        navBar.IsVisible = false;
                        listItems.IsVisible = false;//  !Settings.ShowGetStarted;;;
                        CarouselGettingStartedTour.IsVisible = true;// Settings.ShowGetStarted;;
                           imageTourBackground.IsVisible = true;

                        

                }



                public void HideTourCarousel()
                {
                        navBar.IsVisible = true;
                        listItems.IsVisible = true;//  !Settings.ShowGetStarted;;;
                        CarouselGettingStartedTour.IsVisible = false;
                        imageTourBackground.IsVisible = false;
                        //            IsBusy = true;
                        SetNavigationBarProperties();
                        //                    FABHelper.SetFABProperties(navBar, listItems, ContentContainer, null);
                        //            SetNavigationProperties();





                        navBar.IsVisible = true;
                        SetNavigationBarProperties();

         //               FABHelper.SetFABProperties(navBar, listItems, ContentContainer, "Loading Feed images....");

                        //             ShowTourCarousel();
                        Master = new LeftNavBar() { Title = "Feed" };
                        IsPresented = false;

                        Settings.ShowGetStarted = false;


                }


        public void SetNavigationProperties()
        {
                                                     navBar.IsVisible = true;
            SetNavigationBarProperties();

                                FABHelper.SetFABProperties(navBar, listItems, ContentContainer, "Loading Feed images....");

          //             ShowTourCarousel();
            Master = new LeftNavBar() { Title = "Feed" };
            IsPresented = false;

            MessagingCenter.Subscribe<ViewModelBase>(this, "ShowNavigation", (sender) =>

				{
	IsPresented = true;
});


        }

        public void SetNavigationBarProperties()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("feed");
            navBar.ShowFindFriendsCommand();
           // navBar.ShowNotificationCommand();
            navBar.ShowHamburgerCommand();
        }


    }
}
