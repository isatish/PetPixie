using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FeedViewModel : ViewModelBase
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

        public FeedViewModel(IFeedService feedService, IUserService userService)
        {
            _feedService = feedService;
            _userService = userService;
            _items = new ObservableCollection<FeedItemViewModel>();
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
    }
}