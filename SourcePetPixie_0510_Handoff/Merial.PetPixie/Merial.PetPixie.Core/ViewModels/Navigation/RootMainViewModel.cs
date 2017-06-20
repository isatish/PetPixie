//using MvvmCross.Core.ViewModels;
//using MvvmCross.Forms.Presenter.Core;
//using System;
//using System.Collections.Generic;
//using System.Windows.Input;
//using Merial.PetPixie.Core.Resources;
//using Xamarin.Forms;
//using Merial.PetPixie.Core.Helpers;
//using Merial.PetPixie.Core.Services.Contracts;
//using Merial.PetPixie.Core.ViewModels;
//using Merial.PetPixie.Core.Models;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
//ds using Merial.PetPixie.Core.Resources; Removed temporarily for localization
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    /// <summary>
    /// This will be the ViewModel used in the Master page of the MainPage
    /// The type used in MvxMasterDetailViewModel determines the ViewModel used in the RootContentViewModel
    /// This sample will put a simple ListView in the Master and reacts to changes in 
    /// In every platform: use the right Presenter (MvxFormsDroidMasterDetailPagePresenter, ...)
    /// </summary>

    // public class RootMainViewModel : ViewModelBase  // MvxMasterDetailViewModel<RootContentViewModel>


        public class RootMainViewModel : BaseViewModel
        {


            public RootMainViewModel()
            {
                Profile = new ProfileModel(Settings.CurrentUser.Id);
            //dslocalization        Menu = new[] {
           //dslocalization     new MenuItem { Title = "Feed", IconPath="MainTourFeed.png", Description =  AppResources.Feed,  ViewModelType = typeof(ViewModels.FeedViewModel) },
           //dslocalization         new MenuItem { Title = "Reminders", IconPath="MainTourRemind.png", Description = AppResources.Reminders },
           //dslocalization         new MenuItem { Title = "Discover", IconPath="MainTourDiscover.png", Description = AppResources.Discover, ViewModelType = typeof(DiscoverViewModel) },
           //dslocalization         new MenuItem { Title = "My Pack", IconPath="MainTourPack.png", Description = AppResources.MyPack, ViewModelType = typeof(ProfileDetailBaseViewModel) },
          //dslocalization        //  new MenuItem { Title = "My Pack", IconPath="MainTourPack.png", Description = AppResources.MyPack},
          //dslocalization          new MenuItem { Title = "My vets", IconPath="MainTourVets.png", Description = AppResources.MyVets, ViewModelType = typeof(MyVetsViewModel) }, 
          //dslocalization          new MenuItem { Title = "Settings", IconPath="MainTourSettings.png", Description = AppResources.MyVets, ViewModelType = typeof(SettingsViewModel) }
         //dslocalization           };
            }


            MenuItem _menuItem;

            public MenuItem SelectedMenu
            {
                get { return _menuItem; }
                set
                {
                    if (SetProperty(ref _menuItem, value))
                        OnSelectedChangedCommand.Execute(value);
                }
            }


            ProfileModel _profile;
            public ProfileModel Profile
            {
                get { return _profile; }
                set
                {
                    _profile = value;
                    this.RaisePropertyChanged(() => this.Profile);
                }
            }

            //passed in
            //(IProfileService profileService)

            //    _profileService = profileService;
            //    _profileService.OnProfileChangeEvent += ProfileServiceOnOnProfileChangeEvent;
            //    InitValue();





            IEnumerable<MenuItem> _menu;
            public IEnumerable<MenuItem> Menu { get { return _menu; } set { SetProperty(ref _menu, value); } }

            #region Commands


            private string _displayUsername;
            private string _email;
            private string _picture;

            private readonly IProfileService _profileService;
            public IMvxCommand HomeCommand => new SafeMvxCommand(() => ShowViewModel<FeedViewModel>());
            //  public IMvxCommand DiscoveryCommand => new SafeMvxCommand(() => ShowViewModel<DiscoverViewModel, DiscoverParameter>(typeof(string)));


            //public IMvxCommand DiscoveryCommand => new SafeMvxCommand(() => ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter
            //    {
            //    SearchEntry = ""
            //});



            //public IMvxCommand DiscoveryCommand => new SafeMvxCommand(() => ShowViewModel<DiscoverViewModel>(null));
            public IMvxCommand SettingsCommand => new SafeMvxCommand(() => ShowViewModel<SettingsViewModel>());
            public IMvxCommand MyVetsCommand => new SafeMvxCommand(() => ShowViewModel<MyVetsViewModel>(new { redirect = true }));
         //   public IMvxCommand MyPackCommand => new SafeMvxCommand(() => ShowViewModel<MyPackViewModel, ProfileDetailParameter>(null));
          //  public IMvxCommand DiscoveryCommand => new SafeMvxCommand(() => ShowViewModel<DiscoverViewModel, DiscoverParameter>(null));






            public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);
            private void ShowProfileCommandExecute()
            {
              //  ShowViewModel<ProfileDetailViewModel>();//new { PostId = "123" });
            }


            public IMvxCommand ShowMainCommand => new SafeMvxCommand(ShowMainCommandExecute);
            private void ShowMainCommandExecute()
            {
                //  ShowViewModel<Option2ViewModel>();//new { PostId = "123" });

                // We demand to clear the Navigation stack as we are changing the section
                var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });

                // Show the ViewModel in the Detail NavigationPage
                ShowViewModel(typeof(RootMainViewModel), presentationBundle: presentationBundle);
            }









            MvxCommand<MenuItem> _onSelectedChangedCommand;
            ICommand OnSelectedChangedCommand
            {
                get
                {
                    return _onSelectedChangedCommand ?? (_onSelectedChangedCommand = new MvxCommand<MenuItem>((item) =>
                    {
                        if (item == null)
                            return;

                        var vmType = item.ViewModelType;

                        if (item.ViewModelType == typeof(DiscoverViewModel))
                        {
                            //DiscoveryCommand.Execute();//Models.Enums.SearchMode.User);

                            //ShowViewModel(vmType, presentationBundle: presentationBundle);
                            // ShowViewModel<FeedViewModel>(null);
                            //   ShowViewModel<DiscoverViewModel>("");

                            //ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
                            //{
                            //  //  ProfileId = comment.FromProfileId
                            //});

                            //    ShowViewModel<DiscoverViewModel>(new DiscoverParameter()
                            //{
                            //        SearchMode = Models.Enums.SearchMode.User // ProfileDetailParameter// comment.FromProfileId
                            //    });

                        }
                        else
                        {

                            // We demand to clear the Navigation stack as we are changing the section
                            var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });

                            // Show the ViewModel in the Detail NavigationPage
                            ShowViewModel(vmType, presentationBundle: presentationBundle);
                        }
                    }));
                }
            }

            #endregion




            //public override void RootContentPageActivated()
            //{
            //    // When user go backs to root page in NavigationPage (using UI back or changing option in Menu)
            //    // we unset the SelectedItem of our list
            //    SelectedMenu = null;
            //}


            public class MenuItem
            {
                public string Title { get; set; }
                public string IconPath { get; set; }
                public string Description { get; set; }
                public Type ViewModelType { get; set; }
            }
        }
    }

        
    
