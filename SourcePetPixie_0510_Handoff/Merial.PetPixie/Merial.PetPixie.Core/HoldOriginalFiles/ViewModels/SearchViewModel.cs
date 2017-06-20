using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using KHashTag = Merial.PetPixie.Core.Models.Kinvey.KHashTag;

namespace Merial.PetPixie.Core.ViewModels {
	public class SearchViewModel : ViewModelBase
	{
        #region Private Properties

        private readonly IHashTagService _hashTagService;
	    private readonly IProfileService _profileService;

        private ObservableCollection<KHashTag> _hashTagList;
        private string _searchTxt;
        private SearchMode _searchMode;
	    private ObservableCollection<KProfile> _usersList;
	    private Task _infiniteTask;
	    private CancellationTokenSource _cancelationToken;
	    private bool _noData;

	    #endregion
        
        #region Public Properties

        public ObservableCollection<KHashTag> HashTagList
        {
            get { return _hashTagList; }
            set
            {
                _hashTagList = value;
                RaisePropertyChanged(() => HashTagList);
            }
        }

	    public SearchMode SearchMode
	    {
	        get { return _searchMode; }
	        set
	        {
	            _searchMode = value;
	            
                RaisePropertyChanged(() => SearchMode);
                
                ChangeMode();


            }
	    }

	    

	    public string Hint
	    {
	        get
	        {
	            switch (SearchMode)
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

	            switch (SearchMode)
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


	    public SearchViewModel(IHashTagService hashTagService, IProfileService profileService)
	    {
	        _hashTagService = hashTagService;
	        _profileService = profileService;
	    }

	    public string SearchTxt
	    {
	        get { return _searchTxt; }
	        set
	        {
	            if (_searchTxt != value)
	            {
	                _searchTxt = value;
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

	    public ObservableCollection<KProfile> UsersList
	    {
	        get { return _usersList; }
	        set
	        {
	            _usersList = value;
	            RaisePropertyChanged(() => UsersList);
	        }
	    }

	    #endregion

	    #region MyRegion

	    public IMvxCommand GoDiscoverFromUserSearchCommand => new SafeMvxCommand<KProfile>(GoDiscoverFromUserSearchCore);
	    public IMvxCommand GoDiscoverFromTagSearchCommand => new SafeMvxCommand<KHashTag>(GoDiscoverFromTagSearchCore);

	    private void GoDiscoverFromTagSearchCore(KHashTag tag)
	    {
	        if (tag != null)
	        {
                ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
                {
                    SearchMode = SearchMode.Tag,
                    SearchEntry = tag.Text,
                    TitlePage = "#"+tag.Text
                });
            }
	    }


	    private void GoDiscoverFromUserSearchCore(KProfile profile)
	    {
	        if (profile != null)
	        {
                //ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
                //{
                //    SearchMode = SearchMode.User,
                //       SearchEntry = profile.Id,
                //       TitlePage = profile.FullName
                //});
                ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
                {
                    ProfileId = profile.Id
                });
            }
	    }

	    #endregion

	    #region Protected 

	    #endregion

	    #region Private Methods

	    private void ChangeMode()
	    {
	        RaisePropertyChanged(() => Hint);
            //SearchTxt = "";
	       

            switch (SearchMode)
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
	        switch (SearchMode)
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
	        UsersList = !String.IsNullOrWhiteSpace(SearchTxt) ? new ObservableCollection<KProfile>(await _profileService.GetProfileBySearchText(SearchTxt)) : null;
	    }

	    private async Task UpdateTagList()
	    {
	        HashTagList = !String.IsNullOrWhiteSpace(SearchTxt) ? new ObservableCollection<KHashTag>(await _hashTagService.GetTagByTextAsync(SearchTxt)) : null;
	    }

	    #endregion
	}
}