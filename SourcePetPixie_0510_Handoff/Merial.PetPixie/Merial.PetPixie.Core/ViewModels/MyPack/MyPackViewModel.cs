using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
    public class MyPackViewModel : ProfileDetailBaseViewModel
    {
        void HandleAction()
        {

        }



        #region Fields

        private bool _showList = false;
        private bool _showGrid = true;
        private string _profileId = "";
        private string _petId = "";

        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IPetService _petService;
        private readonly IFeedService _feedService;
        private readonly IBreedService _breedService;

        private ObservableCollection<FeedItemViewModel> _feeds;


        #endregion


        #region Public Constructors

        public MyPackViewModel(IUserService userService, IFriendService friendService, IProfileService profileService, IPetService petService, IFeedService feedService, IBreedService breedService)
             : base(userService, friendService, profileService, petService, feedService, breedService)
        {
            _profileService = profileService;
            //  _mvxPictureChooserTask = mvxPictureChooserTask;     IMvxPictureChooserTask mvxPictureChooserTask,
            _userService = userService;
            _petService = petService;
            _feedService = feedService;
            _breedService = breedService;


            LoadData();
        }


        public List<KBreed> Breeds
        {
            get; set;

        }

        public ObservableCollection<FeedItemViewModel> Feeds
        {
            get { return _feeds; }
            set
            {
                _feeds = value;

                if (Feeds != null && Feeds.Any())
                {
                    IsThereContent = ContentStatus.ContentProvided;
                }
                else
                {
                    IsThereContent = ContentStatus.NoContent;
                }
                RaisePropertyChanged(() => Feeds);
            }
        }

        public async void SetParameters(string profileId, string petId)
        {
            _profileId = profileId;
            IsFetchingData = true;
            //  MajMessages();
            IsThereContent = ContentStatus.Undefined;

            if (Feeds == null || _petId != petId)
            {
                _petId = petId;


                if (_feedService != null)
                    Feeds = new ObservableCollection<FeedItemViewModel>((await _feedService.GetFeedByUserAsync(_profileId, _petId))
                        .Select(kfeed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(kfeed))));

            };

          

            IsThereContent = Feeds.Any() ? ContentStatus.ContentProvided : ContentStatus.NoContent;
            IsFetchingData = false;
        }

        public async Task<bool> LoadBreeds()
        {
            if (Breeds == null)
            {
           var breeds =    await  _breedService.GetBreeds();
                Breeds = breeds;
            }
            return true;
        }

        public async void LoadData()
        {



            var list = await _petService.GetAllAsync(Settings.CurrentUserProfile, true);

            if (list != null && list.Any())
            {
                //  Items = new ObservableCollection<FeedItemViewModel>(
                //      list.Select(feed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(feed))).ToList());

            }

            var profileID = Settings.CurrentUserProfile.Id;
            var petID = Settings.CurrentUserProfile.PetIds.FirstOrDefault();




            SetParameters(profileID, petID);// "");// petID);
           // MessagingCenter.Send(this, "StopLoading");

        }





        #endregion

        #region Properties

        public bool ShowList
        {
            get {
                return _showList; 
            }
            set { _showList = value; RaisePropertyChanged(() => ShowList); }
        }

        public bool ShowGrid
        {
            get {
               return _showGrid;
            }
            set { _showGrid = value; RaisePropertyChanged(() => ShowGrid); }
        }



        public async Task<string> BreedsString(string[] breedIds)
        {
            bool firstTime = true;
            string breedsString = "";



            //var allBreeds = await _breedService.GetBreeds();
            //   var breeds = await _breedService.GetListBreedName(breedIds);
            if (Breeds != null)
            {
                foreach (var breed in Breeds)
                {
                    if (breedIds.Contains(breed.Id))
                    {
                        if (!firstTime)
                        {
                            breedsString += ", ";
                        }
                        firstTime = false;

                        breedsString += breed.Name;

                    }


                }
            }

            return breedsString;

        }


        #endregion



        // Command="{ToggleImageViewCommand}" 

        #region Commands

        public IMvxCommand ToggleImageViewCommand => new SafeMvxCommand(ToggleImageView);


        public IMvxCommand ShowFeedDetails => new SafeMvxCommand<PetModel>((pet) =>
        {
            ShowValidationViewModel<ProfilePetViewModel, ProfilePetParameter>(new ProfilePetParameter()
            {
                EntityMode = EntityMode.Edit,
                PetModel = pet,
            });

        });


        public IMvxCommand ShowProfileCommand => new SafeMvxCommand<FeedModel>((feed) =>
        {

            if (!this.Profile.IsRegisteredInPetPal)
                return;

            ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters
            {
                Feed = feed
            });
        });


        public IMvxCommand AddNewPetsCommand => new SafeMvxCommand(() =>
        {
            ProfilePetViewModel.SelectedBreeds = new List<KBreed>();
            ShowValidationViewModel<ProfilePetViewModel, ProfilePetParameter>(new ProfilePetParameter()
            {
                EntityMode = EntityMode.New
            });
        });


        public IMvxCommand EditPetCommand => new SafeMvxCommand<PetModel>((pet) =>
        {
           ShowValidationViewModel<ProfilePetViewModel, ProfilePetParameter>(new ProfilePetParameter()
           {
               EntityMode = EntityMode.Edit,
               PetModel = pet,
           });

        });




    #region Command actions

    protected void ToggleImageView()
        {
            ShowList = !ShowList;
            ShowGrid = !ShowGrid;
        }

        #endregion

        #endregion
    }
}
