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
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Plugins.PictureChooser;
using MvvmCross.Platform;
using System.IO;
using KinveyXamarin;
using Acr.UserDialogs;
using System;
using Plugin.Media;
using Splat;
using Xamarin.Forms;
//using Merial.PetPixie.Core.ViewModels.Merial.PetPixie.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        #region Fieds

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly IHashTagService _HashService;
        private ObservableCollection<FeedItemViewModel> _items;

        private bool _isRefreshing;
        private bool _isLoading;
        //   private bool _isMenuVisible = false;

        private bool _isLiked = true;
        private bool _isNotLiked = false;



        private readonly IBitmapService _bitmapService;
        private readonly IByteService _byteService;
        private readonly IVideoService _videoService;
        private readonly ICacheService _cacheService;
        private readonly IMediaService _mediaService;
        private readonly INotificationService _notificationService;
        private readonly IReminderService _reminderService;




        private IMvxPictureChooserTask _pictureChooserTask;
        private ObservableCollection<string> _pictureChoiceType;
        private ObservableCollection<string> _mediaChoiceType;
        private string _pictureChoiceTypeSelected;
        private string _mediaChoiceTypeSelected;
        private byte[] _pictureBytes;
        private byte[] _vBytes;
        private bool _showTour;

        #endregion


        #region Constructors

        public RootViewModel()
        {
//            _feedService = feedService;
  //          _userService = userService;
    //        _bitmapService = bitmapService;
      //      _items = new ObservableCollection<FeedItemViewModel>();

        }

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



        #endregion

        #region Commands




 

        #endregion

       





        #region Public Methods

        public override void Start()
        {
            base.Start();


          //  LoadData();
        }

        public override void Started()
        {
            base.Started();
           


        }

        #endregion


        #region Methods


        #endregion

        #region Commands

       #endregion






    }
}