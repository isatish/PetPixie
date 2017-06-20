using System;
using System.Collections.ObjectModel;
using System.Linq;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Models
{
	public class PetModel : EntityBase
    {
        #region Fields

        private string _name;
		private DateTime? _birthday;
		private string _type;
		private string _id;
		private string[] _vetIds;
		private string _imageUrl;
	    private string[] _breedIds;
	    private string _ownerProfileId;
	    private KExpandedImages _kExpandedImages;
        private string _breedsString;
	    private bool _isSelected;
	    private ObservableCollection<ReminderModel> _reminders;

        #endregion

        #region Constructors

        // Used for serialization
        public PetModel()
	    {
	    }

	    public PetModel(KPetWithReminders kPet)
        {  
	        this.Name = kPet.Name;
			this.Birthday = kPet.Birthday;
			this.Type = kPet.Type;
			this.Id = kPet.Id;
			this.VetIds = kPet.VetIds;
			this.ImageUrl = null;
		    this.OwnerProfileId = kPet.OwnerProfileId;
		}

        #endregion

        #region Events

        // Pas réussi à utiliser le OnClick directement dans le ProfileDetailView. Si vous y arrivez, ce serait plus propre...
	    public event System.EventHandler Selected;

	    public event System.EventHandler OpenReminderHandler;

        #endregion

       

        #region Public Properties

        public string Name
        {
			get { return _name; }
			set
            {
				_name = value;
				OnPropertyChanged();
			}
		}

		public DateTime? Birthday
        {
			get {

                if (_birthday == null)
                {
                    _birthday = DateTime.Today;
                }
                return _birthday; 
            }
			set
            {
				_birthday = value;
				OnPropertyChanged();
			}
		}

	    public bool IsSelected
	    {
	        get { return _isSelected; }
	        set
	        {
	            if (_isSelected != value)
	            {
                    _isSelected = value;
                    OnPropertyChanged();
                }
	            
                if (value)
                    this.Selected?.BeginInvoke(this, EventArgs.Empty, null , null);
            }
	    }

	    public string Type
        {
			get { return _type; }
			set
            {
				_type = value;
				OnPropertyChanged();
			}
		}

		public string Id
        {
			get { return _id; }
			set
            {
				_id = value;
				OnPropertyChanged();
			}
		}

		public string[] VetIds
        {
			get { return _vetIds; }
			set
            {
				_vetIds = value; 
				OnPropertyChanged();
			}
		}

		public string ImageUrl
        {
			get { 


                string imageURL = "no_profile_image.png";
                if (_imageUrl != null)
                {
                    imageURL = _imageUrl;


                }

                return imageURL; }
			set
            {
				_imageUrl = value;
				OnPropertyChanged();
			}
		}

	    public KExpandedImages KExpandedImages
	    {
	        get { return _kExpandedImages; }
	        set
	        {
	            _kExpandedImages = value;
                OnPropertyChanged();
            }
        }

	    public string[] BreedIds
	    {
	        get { return _breedIds; }
	        set
	        {
	            _breedIds = value;
                OnPropertyChanged();
            }
	    }

	    public string OwnerProfileId
	    {
	        get { return _ownerProfileId; }
	        set
	        {
	            _ownerProfileId = value;
                OnPropertyChanged();
            }
        }

        public string BreedsString
        {
            get { return _breedsString; }
            set
            {
                _breedsString = value;
                OnPropertyChanged();
            }
        }

	    public FileMetaData FileMetaData { get; set; }

        public KFile Picture { get; set; }

        public KPictures Pictures { get; set; }

	    public ObservableCollection<ReminderModel> Reminders
	    {
            get { return _reminders; }
	        set
	        {
	            _reminders = value;
	            OnPropertyChanged();

                OnPropertyChanged("NbrReminders");
	            _reminders.CollectionChanged += (sender, args) => OnPropertyChanged("NbrReminders");
	        }
	    }

	    public string NbrReminders => $"{Reminders.Count} reminders";

	    public bool IsCurrentProfilePet => this.OwnerProfileId == Settings.CurrentUserProfile.Id;

        #endregion

        #region Public Methods

        public static PetModel CreateFrom(KPetWithReminders kPet, string ownerId =null)
	    {
	        return new PetModel
	        {
	            Id = kPet.Id,
                Name = kPet.Name,
                Birthday = kPet.Birthday,
                Type = kPet.Type,
                VetIds = kPet.VetIds,
                ImageUrl =kPet.ExpandedImages?.KMedium?.DownloadURL,
                BreedIds = kPet.Breed_ids,
                OwnerProfileId = ownerId??kPet.OwnerProfileId,
                Picture = kPet.Picture,
                Pictures = kPet.Pictures,
                Reminders = new ObservableCollection<ReminderModel>(kPet.Reminders?.Select(r=>ReminderModel.CreateFrom(r,kPet.ExpandedImages?.KMedium?.DownloadURL,kPet.Name)) ?? Enumerable.Empty<ReminderModel>())
            };
	    }

	    public KPetWithReminders GenerateKPet()
	    {
	        var kpet = new KPetWithReminders
	        {
	            Id = Id,
                Name = Name,
                Birthday = Birthday,
                Breed_ids =  BreedIds,
                Breeds_ids = BreedIds,
                Type = "dog",
                OwnerProfileId = OwnerProfileId,
                Picture = Picture,
                Pictures = Pictures
	        };

	        if (FileMetaData != null)
	        {
	            kpet.Picture = new KFile
	            {
	                 Id = FileMetaData.id
	            };
	        }

	        return kpet;
	    }

        #endregion
    }
}