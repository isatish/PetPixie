using System.Collections.ObjectModel;
using System.Linq;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;

namespace Merial.PetPixie.Core.Models
{
    public class ProfileModel : EntityBase
    {
        #region Fields

        private readonly IProfileService _profileService;
        private string _connectedProfileId;

        private string _imageSrc;
        private string _userName;
        private string _userFullName;
        private bool _isFollowedByCurrentUser;
        private bool _isLoading;
        private bool _isRegisteredInPetPal;
	    private int _posts;
	    private int _followers;
	    private int _following;
	    private ObservableCollection<PetModel> _pets;
	    private string _aboutMe;
	    private string _country;
        private string _email;
        private string _profileId;
        private string _phoneNumber;

        private string _userId;
	    private bool _isUserBlocked;
        private string _imageSrcLarge;
        private bool _emailSubscription;

        #endregion

        #region Constructors

        public ProfileModel(string connectedProfileId)
        {
            _connectedProfileId = connectedProfileId;
            _profileService = Mvx.Resolve<IProfileService>();
        }

        public ProfileModel()
        {
        }

        #endregion

        #region Public Properties

        public bool IsCurrentUser => this.ProfileId == Settings.CurrentUserProfile.Id;

        public bool IsRegisteredInPetPal
        {
            get { return _isRegisteredInPetPal; }
            set
            {
                if (value == _isRegisteredInPetPal) return;
                _isRegisteredInPetPal = value;
                OnPropertyChanged();
                OnPropertyChanged("ShowUnFollowCommand");
                OnPropertyChanged("ShowFollowCommand");
            }
        }

        public string RelationId { get; set; }

        public string ProfileId
        {
            get { return _profileId; }
            set
            {
                if (value == _profileId) return;
                _profileId = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged("IsCurrentUser");
            }
        }

        public string ImageSrc
        {
            get { return _imageSrc; }
            set
            {
                if (value == _imageSrc) return;
                _imageSrc = value;
                OnPropertyChanged();
            }
        }

        public string ImageSrclarge
        {
            get { return _imageSrcLarge; }
            set
            {
                if (value == _imageSrcLarge) return;
                _imageSrcLarge = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string UserFullName
        {
            get { return _userFullName; }
            set
            {
                if (value == _userFullName) return;
                _userFullName = value;
                OnPropertyChanged();
            }
        }

        public bool IsFollowedByCurrentUser
        {
            get { return _isFollowedByCurrentUser; }
            set
            {
                if (value == _isFollowedByCurrentUser) return;
                _isFollowedByCurrentUser = value;
                OnPropertyChanged();
                OnPropertyChanged("ShowUnFollowCommand");
                OnPropertyChanged("ShowFollowCommand");
            }
        }

        public bool ShowFollowCommand => !this.IsFollowedByCurrentUser && !this.IsLoading && !IsCurrentUser && this.IsRegisteredInPetPal && !IsUserBlocked;

        public bool ShowUnFollowCommand => this.IsFollowedByCurrentUser && !this.IsLoading && !IsCurrentUser && this.IsRegisteredInPetPal && !IsUserBlocked;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged("ShowUnFollowCommand");
                OnPropertyChanged("ShowFollowCommand");
            }
        }

	    public int Posts
        {
		    get { return _posts; }
		    set
            {
			    _posts = value;
			    OnPropertyChanged();
		    }
	    }

	    public int Followers
        {
		    get { return _followers; }
		    set {
			    _followers = value;
			    OnPropertyChanged();
		    }
	    }

	    public int Following
        {
		    get { return _following; }
		    set
            {
			    _following = value;
			    OnPropertyChanged();
		    }
	    }

	    public ObservableCollection<PetModel> Pets
        {
		    get { return _pets; }
		    set
            {
			    _pets = value;
			    OnPropertyChanged();
		    }
	    }

	    public string AboutMe
        {
		    get { return _aboutMe; }
		    set
            {
			    _aboutMe = value;
			    OnPropertyChanged();
		    }
	    }

	    public string Country
        {
		    get { return _country; }
		    set
            {
			    _country = value;
			    OnPropertyChanged();
		    }
	    }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public FileMetaData FileMetaData { get; set; }

	    public bool IsUserBlocked
        {
		    get { return _isUserBlocked; }
		    set
            {
			    _isUserBlocked = value; 
			    OnPropertyChanged();
				OnPropertyChanged("ShowFollowCommand");
				OnPropertyChanged("ShowUnFollowCommand");
			}
	    }

        public bool EmailSubscription
        {
            get { return _emailSubscription; }
            set
            {
                _emailSubscription = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public static ProfileModel CreateFrom(KProfile kprofile,FileMetaData fileMetaData =null)
        {
            if (string.IsNullOrEmpty(kprofile.Id))
            {
                return null;
            }

            ProfileModel profile = new ProfileModel()
            {
                ProfileId = kprofile.Id,
                ImageSrc = kprofile.ExpandedProfilePictures?.KMedium?.DownloadURL,
                ImageSrclarge = kprofile.ExpandedProfilePictures?.KLarge?.DownloadURL?? kprofile.ExpandedProfilePictures?.KMedium?.DownloadURL,
                Email = kprofile.Email,
                UserName = !string.IsNullOrEmpty(kprofile.DisplayUsername) ? kprofile.DisplayUsername : kprofile.FullName,
                UserFullName = !string.IsNullOrEmpty(kprofile.DisplayUsername) ? kprofile.FullName : string.Empty,
                Posts = kprofile.Counts?.Media ?? 0,
                Followers = kprofile.Counts?.FollowedBy ?? 0,
                Following = kprofile.Counts?.Follows ?? 0,
                Pets = new ObservableCollection<PetModel>( kprofile.Pets?.Select(p=>PetModel.CreateFrom(p, kprofile.Id))),
                //TODO Only for debug MCS AKM 
                Country = kprofile.CountryCode??"United States",
                PhoneNumber = kprofile.PhoneNumber,
                _userId =   kprofile.UserId,
                FileMetaData = fileMetaData,
                AboutMe = kprofile.Biography,
                EmailSubscription = kprofile.EmailSubscription
                
            };
          
            return profile;
        }

        public KProfile GenerateKProfile()
        {
            KProfile kprofile = new KProfile
            {
                Id = ProfileId,
                DisplayUsername = UserName,
                Email = Email,
                FullName = UserFullName,
                PhoneNumber = PhoneNumber,
                EmailSubscription = EmailSubscription,
                UserId =  _userId,
                CountryCode = Country
            };
            return kprofile;
        }

        public KProfile UpdateKProfile(KProfile profile)
        {
            profile.DisplayUsername = UserName;
            profile.Email = Email;
            profile.FullName = UserFullName;
            profile.PhoneNumber = PhoneNumber;
            profile.Biography = AboutMe;
            profile.EmailSubscription = EmailSubscription;
            profile.CountryCode = Country;
            if (FileMetaData != null)
            {
                profile.ProfilePicture = new KImage()
                {
                    Id = FileMetaData.id,
                    Type = "KinveyFile",
                    MimeType = "image/jpeg"
                };
            }
            return profile;
        }

        #endregion
    }
}