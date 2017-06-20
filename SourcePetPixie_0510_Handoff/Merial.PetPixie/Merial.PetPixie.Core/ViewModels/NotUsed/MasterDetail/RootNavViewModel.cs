using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;
using System.Collections.Generic;
using System;

namespace Merial.PetPixie.Core.ViewModels
{
    public class RootNavViewModel :MvxMasterDetailViewModel<RootContentViewModel> 
    {
        #region Fieds

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly IHashTagService _HashService;
        private ObservableCollection<FeedItemViewModel> _items;
        
        private bool _isRefreshing;
        private bool _isLoading;

        #endregion

        #region Properties

        public ObservableCollection<FeedItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public static  FeedItemViewModel SelectedItem
        {
            get;set;
           
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

        public IMvxCommand ShowFindFriendsViewCommand
		{
			get
			{
				return new SafeMvxCommand(() => {
					ShowViewModel<FindFriendsTabsViewModel>();
				});
			}
		}

		public ICommand LoadCommand => new MvxCommand(LoadData);

        public ICommand LoadMoreCommand => new MvxCommand(LoadMoreData);

        public ICommand ReloadCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    IsRefreshing = true;

                    ReloadData();

                    IsRefreshing = false;
                });
            }
        }

        public virtual ICommand ItemSelected
        {
            get
            {
                return new SafeMvxCommand<FeedItemViewModel>(item => { /*SelectedItem = item; */});
            }
        }

        #endregion

        #endregion

        public RootNavViewModel(IFeedService feedService, IUserService userService)
        {
            _feedService = feedService;
            _userService = userService;
            _items = new ObservableCollection<FeedItemViewModel>();
        }

        //public override void Start()
        //{
        //    base.Start();
        //    LoadData();
        //}

        public override void Start()
        {
            base.Start();
            if (SelectedItem != null)
            {
                var item =Items.FirstOrDefault(f => f.Feed.PostId == SelectedItem.Feed.PostId);

                if (SelectedItem.IsRemoved && item!=null)
                {
                    Items.Remove(item);
                }else if (item != null)
                {
                    Items[Items.IndexOf(item)].Feed.NbLikes=SelectedItem.Feed.NbLikes;
                    Items[Items.IndexOf(item)].Feed.Likes=SelectedItem.Feed.Likes;
                    Items[Items.IndexOf(item)].Feed.UserHasLiked= SelectedItem.Feed.UserHasLiked;
                    Items[Items.IndexOf(item)].Feed.CommentsCount= SelectedItem.Feed.CommentsCount;
                    Items[Items.IndexOf(item)].Feed = SelectedItem.Feed;
                    Items[Items.IndexOf(item)].Update();
                   
                   
                }
                SelectedItem = null;

            }
        }

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






        #region "MVVM Code logic"

            public class MainViewModel : MvxMasterDetailViewModel<RootContentViewModel>
        {
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

            IEnumerable<MenuItem> _menu;
            public IEnumerable<MenuItem> Menu { get { return _menu; } set { SetProperty(ref _menu, value); } }

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

                    // We demand to clear the Navigation stack as we are changing the section
                    var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });

                    // Show the ViewModel in the Detail NavigationPage
                    ShowViewModel(vmType, presentationBundle: presentationBundle);
                    }));
                }
            }

            public MainViewModel()
            {//
               // Menu = new[] {
              //  new MenuItem { Title = "Opción 1", Description = "Descripción Opción 1", ViewModelType = typeof(Option1ViewModel) },
               // new MenuItem { Title = "Opción 2", Description = "Descripción Opción 2", ViewModelType = typeof(Option2ViewModel) },
              //  new MenuItem { Title = "Opción 3", Description = "Descripción Opción 3", ViewModelType = typeof(Option3ViewModel) }
           // };
            }

            public override void RootContentPageActivated()
            {
                // When user go backs to root page in NavigationPage (using UI back or changing option in Menu)
                // we unset the SelectedItem of our list
                SelectedMenu = null;
            }
        }

        public class MenuItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public Type ViewModelType { get; set; }
        }
        #endregion



    }
}