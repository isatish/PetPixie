using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
//dslocalization using Merial.PetPixie.Core.Resources;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Merial.PetPixie.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region Public Fields

        public string AppVersion
            => Mvx.Resolve<IDeviceInfoService>().GetBundleVersion();

        public class MenuItem
        {
            public string Title { get; set; }
            public string IconPath { get; set; }
            public string Description { get; set; }
            public Type ViewModelType { get; set; }
            public string ViewModelSubType { get; set; }
        }

        IEnumerable<MenuItem> _menu;
        public IEnumerable<MenuItem> Menu { get { return _menu; } set { SetProperty(ref _menu, value); } }
        public IEnumerable<MenuItem> SettingsGeneral { get { return _menu; } set { SetProperty(ref _menu, value); } }
       
        public IEnumerable<MenuItem> SettingsSupport
        {
            get
            {
                IEnumerable<MenuItem> menuItems;
                menuItems = new[] {
                new MenuItem { Title = "About", IconPath="ic_about.png", Description =  "About Pet Pixie",  ViewModelType = typeof(ViewModels.WebViewViewModel), ViewModelSubType="About" },
             //dslocalization       new MenuItem { Title = "Terms Of Use", IconPath="ic_tou.png", Description = AppResources.Reminders, ViewModelType = typeof(ViewModels.WebViewViewModel), ViewModelSubType="TOU" },
             //dslocalization      new MenuItem { Title = "Privacy", IconPath="ic_privacy.png", Description = AppResources.Discover, ViewModelType = typeof(ViewModels.WebViewViewModel), ViewModelSubType="Privacy" },
             //dslocalization      new MenuItem { Title = "Guidelines", IconPath="ic_guidelines.png", Description = AppResources.MyVets, ViewModelType = typeof(ViewModels.WebViewViewModel), ViewModelSubType="Guidelines" }
                };
                return menuItems;
            }
        }


        public IEnumerable<MenuItem> SettingsUsers
        {
            get
            {
                IEnumerable<MenuItem> menuItems;
                menuItems = new[] {
                  new MenuItem { Title = "Log Out", IconPath="ic_logout.png", Description= "Log out" }
                };
                return menuItems;
            }
        }

        public IEnumerable<MenuItem> SettingsFeedback
        {
            get
            {
                IEnumerable<MenuItem> menuItems;
                menuItems = new[] {
                  new MenuItem { Title = "Send Feedback", IconPath="ic_feedback.png", Description= "My Vets" }
                };
                return menuItems;
            }
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

                    if (item.ViewModelType == typeof(WebViewViewModel))
                    {
                        switch (item.ViewModelSubType)
                        {
                            case "About":
                                ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
                                {
                                    Uri = Constants.AppUrl.About,
                                    Title = "About Pet+Pixie"
                                });
                                break;

                            case "Guidelines":
                                ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
                                {
                                    Uri = Constants.AppUrl.GuideLines,
                                    Title = "Guidelines"
                                });
                                break;

                            case "Privacy":
                                ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
                                {
                                    Uri = Constants.AppUrl.Privacy,
                                    Title = "Privacy"
                                });
                                break;

                            case "TOU":
                                ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
                                {
                                    Uri = Constants.AppUrl.TermOfUse,
                                    Title = "Terms of Use"
                                });
                            break;
                        }

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

        #region Constructors

        public SettingsViewModel(IUserService userService)
        {
            _userService = userService;

            SettingsGeneral = new[] {
               new MenuItem { Title = "Notifications", IconPath="ic_general.png", Description = "Notifications" }
                };

            //SettingsSupport = new[] {
            //    new MenuItem { Title = "About", IconPath="MainTourFeed.png", Description =  AppResources.Feed,  ViewModelType = typeof(ViewModels.FeedViewModel) },
            //    new MenuItem { Title = "Terms Of Use", IconPath="MainTourRemind.png", Description = AppResources.Reminders, ViewModelType = typeof(Option2ViewModel) },
            //    new MenuItem { Title = "Privacy", IconPath="MainTourDiscover.png", Description = AppResources.Discover, ViewModelType = typeof(DiscoverViewModel) },
            //    new MenuItem { Title = "Guidelines", IconPath="MainTourVets.png", Description = AppResources.MyVets, ViewModelType = typeof(Option3ViewModel) }
            //    };

            //SettingsUsers = new[] {
            //   new MenuItem { Title = "Logout", IconPath="MainTourVets.png", Description = AppResources.MyVets, ViewModelType = typeof(Option3ViewModel) }
            //    };

            //SettingsFeedback = new[] {
            //   new MenuItem { Title = "Send Feedback", IconPath="MainTourVets.png", Description = AppResources.MyVets, ViewModelType = typeof(Option3ViewModel) }
            //    };


            //SettingsSupport = new[] {
            //    new MenuItem { Title = "About", IconPath="MainTourFeed.png", Description =  AppResources.Feed,  ViewModelType = typeof(ViewModels.FeedViewModel) },
            //    new MenuItem { Title = "Terms Of Use", IconPath="MainTourRemind.png", Description = AppResources.Reminders, ViewModelType = typeof(Option2ViewModel) },
            //    new MenuItem { Title = "Privacy", IconPath="MainTourDiscover.png", Description = AppResources.Discover, ViewModelType = typeof(DiscoverViewModel) },
            //    new MenuItem { Title = "Guidelines", IconPath="MainTourVets.png", Description = AppResources.MyVets, ViewModelType = typeof(Option3ViewModel) }
            //    };
        }

        #endregion

        #region Events

        public EventHandler LoggedOut;

        #endregion

        #region Commands

        public IMvxCommand ShowAboutCommand => new SafeMvxCommand(DoShowAboutCommand);
        private void DoShowAboutCommand()
        {
           // ShowViewModel<WebViewViewModel>();
            ShowViewModel<WebViewViewModel,WebViewParameter>(new WebViewParameter()
            {
                Uri = Constants.AppUrl.About,
                Title = "About Pet+Pixie"
            });
        }

        public IMvxCommand ShowTermsOfUseCommand => new SafeMvxCommand(DoShowTermsOfUseCommand);
        private void DoShowTermsOfUseCommand()
        {
            ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
            {
                Uri = Constants.AppUrl.TermOfUse,
                Title = "Terms of use",
            });
        }


        public IMvxCommand ShowPrivacyCommand => new SafeMvxCommand(DoShowPrivacyCommand);
        private void DoShowPrivacyCommand()
        {
            ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
            {
                Uri = Constants.AppUrl.Privacy,
                Title = "Privacy"
            });
        }


        public IMvxCommand ShowGuideLinesCommand => new SafeMvxCommand(DoShowGuideLinesCommand);
        private void DoShowGuideLinesCommand()
        {
            ShowViewModel<WebViewViewModel, WebViewParameter>(new WebViewParameter()
            {
                Uri = Constants.AppUrl.GuideLines,
                Title = "Guidelines"
            });
        }

        public IMvxCommand ShowNotificationSettingsCommand => new SafeMvxCommand(DoShowNotificationSettingsCommand);
        private void DoShowNotificationSettingsCommand()
        {
            ShowViewModel<NotificationSettingsViewModel>();
        }

        public IMvxCommand ShowLogoutCommand => new SafeMvxCommandAsync(DoLogoutCommand);
        private async Task DoLogoutCommand()
        {
            if (await PopupService
                .DisplayYesNoMessage("Confirmation", $"Are you sure you want to logout your account ?",
                    "Yes", "No"))
            {
                this._userService.Logout();

                if (this.LoggedOut != null && this.LoggedOut.GetInvocationList().Any())
                    foreach (EventHandler target in this.LoggedOut.GetInvocationList())
                        target.BeginInvoke(this, EventArgs.Empty, null, null);

                var presentationBundle =
                    new MvxBundle(new Dictionary<string, string>
                    {
                        {Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack}
                    });

                 Settings.ShowGetStarted = true;
                ShowViewModel<LoginViewModel>(new { flag = true },presentationBundle: presentationBundle);
            }
        }

        #endregion
    }
}