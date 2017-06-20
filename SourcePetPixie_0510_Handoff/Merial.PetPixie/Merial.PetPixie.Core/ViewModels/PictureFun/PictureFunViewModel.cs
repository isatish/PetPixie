using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels;
//using Merial.PetPixie.Core.ViewModels.Merial.PetPixie.Core.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using MvvmCross.Platform;
using Merial.PetPixie.Core.Views;
//using MR.Gestures;

namespace Merial.PetPixie.Core.ViewModels
{
    public class PictureFunViewModel : ViewModelBase
    {
        #region Fieds

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly IHashTagService _HashService;
        private ObservableCollection<FeedItemViewModel> _items;

        private bool _isRefreshing;
        private bool _isLoading;
        private bool _isMenuVisible = false;

        private int _red = 0;
        private int _green = 0;
        private int _blue = 0;


        #endregion

        #region Properties


        public int Red
        {
            get { return _red; }
            set
            {
                if (value != _red)
                {
                    _red = value;
                    RaisePropertyChanged(() => Red);
                    //   this.RaiseAndSetIfChanged(ref _red, value); }
                }
            }
        }


        public int Green
        {
            get { return _green; }
            set
            {
                if (value != _green)
                {
                    _green = value;
                    RaisePropertyChanged(() => Red);
                    //   this.RaiseAndSetIfChanged(ref _red, value); }
                }

                //this.RaiseAndSetIfChanged(ref _green, value); 
            }
        }
    

 
        public int Blue
        {
            get { return _blue; }
            set
            {
                if (value != _blue)
                {
                    RaisePropertyChanged(() => Blue);
                    //   this.RaiseAndSetIfChanged(ref _red, value); }
                }


                // this.RaiseAndSetIfChanged(ref _blue, value); 
            }
        }

        ObservableAsPropertyHelper<Color> _color;
        public Color Color
        {
            get { return _color.Value; }
        }

        //public ColorSlider()
        //{
        //    this.WhenAnyValue(
        //        x => x.Red, x => x.Green, x => x.Blue,
        //        (red, green, blue) => Color.FromArgb(255, red, green, blue)
        //    )
        //    .ToProperty(this, v => v.Color, out _color);
        //}



        public string FunImageSource
        {
            get { return Settings.CurrentFunImageSource; }

        }


        private string _funItemImageSource = "clothes_1.png";
        public string FunItemImageSource
        {
            get 
            { 
                return _funItemImageSource; 
            }

            set
            {
                _funItemImageSource = value;
               RaisePropertyChanged(() => FunItemImageSource);

            }

        }



        public ObservableCollection<FeedItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public static FeedItemViewModel SelectedItem
        {
            get; set;

        }

 

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public bool IsMenuVisibile
        {
            get { return _isMenuVisible; }
            set
            {
                _isMenuVisible = value;
                RaisePropertyChanged(() => IsMenuVisibile);
            }
        }

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }



        

        #region Commands




        public IMvxCommand ShareFunCommand => new SafeMvxCommand(ShareFun);

        private void ShareFun()
        {
            MessagingCenter.Send(this,"SaveFun");
            ShowViewModel<ShareViewModel>();
        }







        public IMvxCommand AcceptFunCommand => new SafeMvxCommand(AcceptFun);

        private void AcceptFun()
        {
            MessagingCenter.Send(this, "SaveFun");
            ShowViewModel<ShareViewModel>();
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




        #endregion

        #endregion



        public PictureFunViewModel(IFeedService feedService, IUserService userService)
        {
            _feedService = feedService;
            _userService = userService;
            _items = new ObservableCollection<FeedItemViewModel>();

            var funMetaData = new FunMetadata();








        //    App.FunPictureBytes = Mvx.Resolve<IImageService>().MergePictures(App.FunPictureBytes, funMetaData );

            //Color.WhenAnyValue(
            //    x => x.Red, x => x.Green, x => x.Blue,
            //    (red, green, blue) => Color.FromRgba(red, green, blue,255)
            //)
            //.ToProperty(this, v => v.Color, out _color);
        }

        public override void Start()
        {
            base.Start();
            LoadData();
        }

        public override void Started()
        {
            base.Started();
            if (SelectedItem != null)
            {
                var item = Items.FirstOrDefault(f => f.Feed.PostId == SelectedItem.Feed.PostId);

                if (SelectedItem.IsRemoved && item != null)
                {
                    Items.Remove(item);
                }
                else if (item != null)
                {
                    Items[Items.IndexOf(item)].Feed.NbLikes = SelectedItem.Feed.NbLikes;
                    Items[Items.IndexOf(item)].Feed.Likes = SelectedItem.Feed.Likes;
                    Items[Items.IndexOf(item)].Feed.UserHasLiked = SelectedItem.Feed.UserHasLiked;
                    Items[Items.IndexOf(item)].Feed.CommentsCount = SelectedItem.Feed.CommentsCount;
                    Items[Items.IndexOf(item)].Feed = SelectedItem.Feed;
                    Items[Items.IndexOf(item)].Update();


                }
                SelectedItem = null;

            }

      


        }

        //private void PictureAvailable(Stream stream)
        //{
        //    Mvx.Resolve<IImageService>().CropPicture(stream, PictureAvailableAfterCrop, AssumeCancelled, true);
        //}

        #region Methods

        public async void LoadData()
        {
            if (IsLoading == true)
                return;
            IsLoading = true;


            var list = await _feedService.GetAllAsync();
            if (list != null && list.Any())
            {
                Items = new ObservableCollection<FeedItemViewModel>(
                    list.Select(feed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(feed))).ToList());

            }

            IsLoading = false;

        }

        public virtual async void LoadMoreData()
        {
            if (IsLoading == true)
                return;

            IsLoading = true;

            int skip = Items.Count;
            var list = await _feedService.GetAllAsync(skip);

            for (int i = 0; i < list.Count - 1; i++)
            {
                Items.Add(new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(list[i])));
            }
            IsLoading = false;
        }

        public void ReloadData()
        {
            LoadData();
        }

        #endregion



        #region Commands

        public IMvxCommand ShowAboutCommand => new SafeMvxCommand(DoShowAboutCommand);
        private void DoShowAboutCommand()
        {
            IsMenuVisibile = !IsMenuVisibile;
        }


        public IMvxCommand ShowSettingsCommand => new SafeMvxCommand(DoShowSettingsCommand);
        private void DoShowSettingsCommand()
        {
            ShowViewModel<SettingsViewModel>();
        }


        public IMvxCommand ShowFeedCommand => new SafeMvxCommand(DoShowFeedCommand);
        private void DoShowFeedCommand()
        {
            ShowViewModel<FeedViewModel>();
        }


        public IMvxCommand ShowDiscoverCommand => new SafeMvxCommand(DoShowDiscoverCommand);
        private void DoShowDiscoverCommand()
        {

            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = "",// tag.Text,
                TitlePage = "#" + "dog"// tag.Text
            });


        }



        public IMvxCommand ShowMyPackCommand => new SafeMvxCommand(DoShowMyPackCommand);
        private void DoShowMyPackCommand()
        {
        //    ShowViewModel<MyPackViewModel>();
            ShowViewModel<MyPackViewModel, ProfileDetailParameter>(null);
        }

        public IMvxCommand ShowRemindersCommand => new SafeMvxCommand(DoShowRemindersCommand);
        private void DoShowRemindersCommand()
        {
            //ShowViewModel<ViewModel>();
        }




        #endregion
    }
}