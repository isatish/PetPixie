using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
    public class DiscoverSearchViewModel : ViewModelBase<DiscoverSearchParameter>
    {
        #region Fields

        private const int ListCount = 3;
        private const int _nbImageGroupsToLoad = 10;// 3;
        private readonly IFeedService _feedService;
        private readonly IHashTagService _hashTagService;
        private readonly IUserService _userService;
        private bool _isLoading;
        private bool _isRefreshing;
        private ObservableCollection<DiscoverItemViewModel> _items;
        private string _searchHashTag;
        private readonly IImageService _imageService;
        private int _yetLoaded = 0;
        private bool _searchVisible = false;
        private bool _showHashTagSearch = true;
        private bool _showUserNameSearch = false;
        private bool _userSearchResultsVisible = false;
        private bool _tagSearchResultsVisible = false;
        private string _searchText = "";
        private SearchMode CurrentSearchMode = SearchMode.Tag;
        #endregion

        #region Private Properties

      //  private readonly IHashTagService _hashTagService;
        private readonly IProfileService _profileService;

        private ObservableCollection<KHashTag> _hashTagList;
        private string _searchTxt;
        private SearchMode _searchMode;
        private ObservableCollection<KProfile> _usersList;
        private Task _infiniteTask;
        private CancellationTokenSource _cancelationToken;
        private bool _noData;
        private bool _showImages = true;

        #endregion




        #region Constructors

        public DiscoverSearchViewModel(
            IFeedService feedService,
            IUserService userService,
            IHashTagService hashTagService,
            IImageService imageService,
            IProfileService profileService)
        {
            _feedService = feedService;
            _userService = userService;
            _hashTagService = hashTagService;
            _imageService = imageService;
            _profileService = profileService;
            _items = new ObservableCollection<DiscoverItemViewModel>();
            _searchHashTag = string.Empty;
        }

        #endregion

        #region Events

        public event EventHandler OnDataLoaded;
        public event EventHandler OnFocused;

        #endregion

        #region Properties


        public DiscoverSearchParameter DiscoverSearchParameter { get; private set; }

        public ObservableCollection<DiscoverItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }


        public bool TagSearchResultsVisible
        {
            get
            {
                return _tagSearchResultsVisible;
            }
            set
            {
                _tagSearchResultsVisible = value;
                RaisePropertyChanged(() => TagSearchResultsVisible);
            }


        }

        public bool ShowImages
        {
            get
            {
                return _showImages;
            }
            set
            {
                _showImages = value;
                RaisePropertyChanged(() => ShowImages);
            }

        }


        public bool UserSearchResultsVisible
        {
            get
            {
                return _userSearchResultsVisible;
            }
            set
            {
                _userSearchResultsVisible = value;
                RaisePropertyChanged(() => UserSearchResultsVisible);
            }

        }


        public bool SearchVisible
        {
            get
            {
                return _searchVisible;
            }
            set
            {
                _searchVisible = value;
                RaisePropertyChanged(() => SearchVisible);
            }
        }

        public bool ShowHashTagSearch
        {
            get
            {
                return _showHashTagSearch;
            }
            set
            {
                _showHashTagSearch = value;
                RaisePropertyChanged(() => ShowHashTagSearch);
            }
        }

        public bool ShowUserNameSearch
        {
            get
            {
                return _showUserNameSearch;
            }
            set
            {
                _showUserNameSearch = value;
                RaisePropertyChanged(() => ShowUserNameSearch);
            }
        }


 
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);

                //if (value == true)
                //{
                //    try
                //    {
                //        InvokeOnMainThread(() =>
                //         {
                //             Mvx.Resolve<IUserDialogs>().ShowLoading("Discovering images");
                //         });
                //    }
                //    catch (Exception exc)
                //    {
                //        string except = exc.Message;
                //    }
                //}
                //else
                //{
                //    Mvx.Resolve<IUserDialogs>().HideLoading();
                //}

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

        #endregion

        #region Commands

        public IMvxCommand ShowProfileCommand => new SafeMvxCommand<FeedModel>((feed) =>
{

            //if (!this.Profile.IsRegisteredInPetPal)
            //    return;

            ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters
    {
        Feed = feed
    });
});

        public ICommand BackCommand => new SafeMvxCommand(() => Close(this));

        public ICommand LoadMoreCommand => new SafeMvxCommandAsync(LoadMoreData);

        public ICommand ReloadCommand => new SafeMvxCommandAsync(async () => await LoadData(true));

      //  public IMvxCommand SearchCommand => new SafeMvxCommand(DoSearchCommand);

        public IMvxCommand ShowSearchAreaCommand => new SafeMvxCommand(DoShowSearchAreaCommand);

        public IMvxCommand ToggleSearchTypeCommand => new SafeMvxCommand(DoToggleSearchTypeCommand);

      //  public IMvxCommand GoDiscoverFromUserSearchCommand => new SafeMvxCommand<KProfile>(GoDiscoverFromUserSearchCore);
      //  public IMvxCommand GoDiscoverFromTagSearchCommand => new SafeMvxCommand<KHashTag>(GoDiscoverFromTagSearchCore);

        public IMvxCommand GoDiscoverFromTagSearchCommand => new SafeMvxCommand<KHashTag>(GoDiscoverFromTagSearch);
        public IMvxCommand GoDiscoverFromUserSearchCommand => new SafeMvxCommand<KProfile>(GoDiscoverFromUserSearch);


        private void GoDiscoverFromTagSearch(KHashTag tag)
        {
            if (tag != null)
            {
                SearchVisible = false;
                TagSearchResultsVisible = false;

                DiscoverSearchParameter = new DiscoverSearchParameter();
                DiscoverSearchParameter.SearchMode = SearchMode.Tag;
                DiscoverSearchParameter.SearchEntry = tag.Text;
                LoadSearchData();
            }
        }


        private void GoDiscoverFromUserSearch(KProfile profile)
        {
            
            if (profile != null)
            {
                SearchVisible = false;
                UserSearchResultsVisible = false;

                DiscoverSearchParameter = new DiscoverSearchParameter();
                DiscoverSearchParameter.SearchMode = SearchMode.User;
                DiscoverSearchParameter.SearchEntry = profile.UserId;//.Text;
                LoadSearchData();

            }
        }




        #endregion

        #region Public Methods

        public override void Start()
        {
            base.Start();
            LoadData();
        }

        public override void Started()
        {
            base.Started();
            if (FeedViewModel.SelectedItem != null && FeedViewModel.SelectedItem.IsRemoved)
            {
                LoadData();
                FeedViewModel.SelectedItem = null;
            }
        }




        public async Task LoadSearchData(bool isRefreshing = false)
        {
            ShowImages = false;
            if (isRefreshing)
                IsRefreshing = true;
            else
                IsLoading = true;

            _yetLoaded = 0;
            Items.Clear();
            IsThereContent = ContentStatus.Undefined;
            List<KFeed> discoverItems = null;

            if (DiscoverSearchParameter != null)
            {
                switch (CurrentSearchMode)
                {
                    case SearchMode.Tag:
                    //    ShowImages = false;
                        discoverItems = await _feedService.GetAllDiscoverByTagAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    case SearchMode.User:
                      //  ShowImages = false;

                        discoverItems = await _feedService.GetAllDiscoverByUserAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                discoverItems = await _feedService.GetAllDiscoverAsync(_yetLoaded);
            }

            if (discoverItems == null || !discoverItems.Any())
            {
                IsThereContent = ContentStatus.NoContent;
            }
            else
            {
               LoadImages(discoverItems);
                IsThereContent = ContentStatus.ContentProvided;
                ShowImages = true;
            }
            if (isRefreshing)
                IsRefreshing = false;
            else
                IsLoading = false;
        }




        public async Task LoadData(bool isRefreshing = false)
        {
            if (isRefreshing)
                IsRefreshing = true;
            else
                IsLoading = true;

            _yetLoaded = 0;
            Items.Clear();
            IsThereContent = ContentStatus.Undefined;
            List<KFeed> discoverItems = null;

            if (DiscoverSearchParameter != null)
            {
                switch (DiscoverSearchParameter.SearchMode)
                {
                    case SearchMode.Tag:
                        discoverItems = await _feedService.GetAllDiscoverByTagAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    case SearchMode.User:
                        discoverItems = await _feedService.GetAllDiscoverByUserAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                discoverItems = await _feedService.GetAllDiscoverAsync(_yetLoaded);
            }

            if (discoverItems == null || !discoverItems.Any())
            {
                IsThereContent = ContentStatus.NoContent;
            }
            else
            {
                LoadImages(discoverItems);
                IsThereContent = ContentStatus.ContentProvided;
            }
            if (isRefreshing)
                IsRefreshing = false;
            else
                IsLoading = false;
        }

        public virtual async Task LoadMoreData()
        {
            if (IsLoading || IsRefreshing) return;

            IsThereContent = ContentStatus.Undefined;
            IsLoading = true;
            List<KFeed> list = null;
            if (DiscoverSearchParameter != null)
            {
                switch (DiscoverSearchParameter.SearchMode)
                {
                    case SearchMode.Tag:
                        list = await _feedService.GetAllDiscoverByTagAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    case SearchMode.User:
                        list = await _feedService.GetAllDiscoverByUserAsync(DiscoverSearchParameter.SearchEntry, _yetLoaded);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                list = await _feedService.GetAllDiscoverAsync(_yetLoaded);
            }
            if ((Items == null || !Items.Any()) && (list == null || !list.Any()))
                IsThereContent = ContentStatus.NoContent;
            else if (list.Count > 0)
            {
                LoadImages(list);
                IsThereContent = ContentStatus.ContentProvided;
            }

            IsLoading = false;
        }

        #endregion



        public string SearchTxt
        {
            get { return _searchTxt; }
            set
            {
                if (_searchTxt != value)
                {
                    _searchTxt = value;
                    SearchVisible = true;


                   // ShowHashTagSearch = true;
                   // TagSearchResultsVisible = true;
                    RaisePropertyChanged(() => SearchTxt);
                    WaitToUpdateSearchValue();
                }
            }
        }

        private async void WaitToUpdateSearchValue()
        {
            if (_infiniteTask != null)
            {
                IsFetchingData = true;
                RaisePropertyChanged(() => NoData);
                _cancelationToken.Cancel();
            }
            if (!IsFetchingData)
                IsFetchingData = true;
            _cancelationToken = new CancellationTokenSource();
            _infiniteTask = Task.Delay(Int32.MaxValue, _cancelationToken.Token);

            if (await Task.WhenAny(_infiniteTask, Task.Delay(2000)) != _infiniteTask)
                await UpdateSearch();

            IsFetchingData = false;
            RaisePropertyChanged(() => NoData);
        }

        public string Hint
        {
            get
            {
                switch (CurrentSearchMode)
                {
                    case SearchMode.Tag:
                        return "Search for hashtags";
                    case SearchMode.User:
                        return "Search for users";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        public bool NoData
        {
            get
            {

                if (IsFetchingData == true)
                    return false;

                switch (CurrentSearchMode)
                {
                    case SearchMode.Tag:
                        return (HashTagList == null || !HashTagList.Any());
                        break;
                    case SearchMode.User:
                        return (UsersList == null || !UsersList.Any());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        public ObservableCollection<KHashTag> HashTagList
        {
            get { return _hashTagList; }
            set
            {
                _hashTagList = value;
                RaisePropertyChanged(() => HashTagList);
            }
        }
        public ObservableCollection<KProfile> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                RaisePropertyChanged(() => UsersList);
            }
        }

        #region Private Methods

        private void ChangeMode()
        {
            RaisePropertyChanged(() => Hint);
            //SearchTxt = "";


            switch (CurrentSearchMode)
            {
                case SearchMode.Tag:
                    UsersList = null;
                    break;
                case SearchMode.User:
                    HashTagList = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            WaitToUpdateSearchValue();
        }


        private async Task UpdateSearch()
        {
            switch (CurrentSearchMode)
            {
                case SearchMode.Tag:
                    await UpdateTagList();
                    break;
                case SearchMode.User:
                    await UpdateUserList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private async Task UpdateUserList()
        {
            TagSearchResultsVisible = false;
            UserSearchResultsVisible = true;
            UsersList = !String.IsNullOrWhiteSpace(SearchTxt) ? new ObservableCollection<KProfile>(await _profileService.GetProfileBySearchText(SearchTxt)) : null;
        }

        private async Task UpdateTagList()
        {
            TagSearchResultsVisible = true;
            UserSearchResultsVisible = false;
            HashTagList = !String.IsNullOrWhiteSpace(SearchTxt) ? new ObservableCollection<KHashTag>(await _hashTagService.GetTagByTextAsync(SearchTxt)) : null;
        }

        #endregion

        #region Methods



        private void DoToggleSearchTypeCommand()
        {
            ShowImages = false;
            if (CurrentSearchMode == SearchMode.User)
            {
                ShowUserNameSearch = false;
                ShowHashTagSearch = true;
                CurrentSearchMode = SearchMode.Tag;
            }
            else
            {
                ShowUserNameSearch = true;
                ShowHashTagSearch = false;
                CurrentSearchMode = SearchMode.User;
            }
        }

        private void DoShowSearchAreaCommand()
        {
            SearchVisible = true;

//            ShowViewModel<SearchViewModel>();
        }


        //private void DoSearchCommand()
        //{
        //    SearchVisible = true;
        //    ShowHashTagSearch = true;
        //  //  ShowViewModel<SearchViewModel>();

        //    DiscoverParameter.SearchMode = SearchMode.Tag;
        //    DiscoverParameter.SearchEntry =  SearchText;
        //    LoadSearchData();
       
        //}

        protected override void RealInit(DiscoverSearchParameter parameter)
        {
            DiscoverSearchParameter = parameter;
            MessagingCenter.Send<DiscoverSearchViewModel, string>(this, "SearchTag", parameter.SearchEntry);
            GoDiscoverFromTagSearch(new KHashTag() { Text = parameter.SearchEntry.Replace("#", "") });
                                    
        }

        private void LoadImages(List<KFeed> discoverItems)
        {
            var listDiscover = new List<DiscoverItemViewModel>(); //List of images discover 3 by 3
            var temp = new List<KFeed>();
            var localYetLoaded = 0;

            //Loading 3(_nbImageGroupsToLoad) Packages of 3(ListCount) images
            for (var i = 0; i < _nbImageGroupsToLoad * 2; i++)
            {
               
                // If at least X images (packet of 3 by default)
                if (discoverItems.Count - localYetLoaded >= ListCount)
                {
                    for (var j = 0; j < ListCount; j++)
                    {
                        temp.Add(discoverItems[localYetLoaded]);
                        localYetLoaded++;
                    }
                    listDiscover.Add(PrepareItemList(temp));
                }
                temp.Clear();
            }


            // Load remaining images (if less than 9 (_nbImageGroupsToLoad * ListCount))
            if ((localYetLoaded + 1 < ListCount) && (discoverItems.Count >= localYetLoaded + 1))
            {
                var stillToLoad = ListCount - (localYetLoaded + 1);
                temp = new List<KFeed>();
                var discover = new DiscoverItemViewModel();

                if (stillToLoad > 1)
                {
                    temp.Add(discoverItems[localYetLoaded]);
                    localYetLoaded++;

                    discover.Image1 = temp.Select(FeedModel.CreateFrom).ToList()[0];
                    discover.Choice = 3;
                }

                if (stillToLoad > 2)
                {
                    temp.Add(discoverItems[localYetLoaded + 1]);
                    localYetLoaded++;
                    discover.Image1 = temp.Select(FeedModel.CreateFrom).ToList()[1];
                }
                listDiscover.Add(discover);
            }

            if (Items == null || Items.Count == 0)
            {
                Items = new ObservableCollection<DiscoverItemViewModel>(listDiscover);
            }
            else
            {
                foreach (var element in listDiscover)
                {
                    Items.Add(element);
                }
            }

            _yetLoaded += localYetLoaded;
        }

        private DiscoverItemViewModel PrepareItemList(List<KFeed> someDiscoverItems)
        {
            var rnd = new Random();
            var discover = new DiscoverItemViewModel();
            var discoverList = someDiscoverItems.Select(FeedModel.CreateFrom).ToList();

            if (discoverList.Count > 0)
                discover.Image1 = discoverList[0];
            if (discoverList.Count > 1)
                discover.Image2 = discoverList[1];
            if (discoverList.Count > 2)
                discover.Image3 = discoverList[2];

            discover.Choice = rnd.Next(1, 3);

            return discover;
        }

        #endregion
    }

    public class DiscoverSearchParameter
    {
        public SearchMode SearchMode { get; set; }

        public string SearchEntry { get; set; }

        public string TitlePage { get; set; } = "DiscoverSearch";
    }
}