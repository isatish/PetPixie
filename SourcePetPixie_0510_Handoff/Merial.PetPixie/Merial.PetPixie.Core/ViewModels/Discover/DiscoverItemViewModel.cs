using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class DiscoverItemViewModel : ViewModelBase
    {
        #region Fields

        private FeedModel _image1;
        private FeedModel _image2;
        private FeedModel _image3;
        private bool _showLtoR = false;
        private bool _showRtoL = false;

        private int _choice;

        #endregion

        #region constructors


           public DiscoverItemViewModel()
        {

            Settings.ShowLtoR = !Settings.ShowLtoR;
            _showLtoR = Settings.ShowLtoR;
            _showRtoL = !_showLtoR;
        }


        #endregion

        #region Public Properties

        public bool ShowRtoL
        {
            get
            {

                return _showRtoL;
            }

        }

        public bool ShowLtoR
        {
            get
            {
                 return _showLtoR;
            }

        }

        private int _totalLikes = 0;
          public int TotalLikes
        {
            get { return Image1.NbLikes; }
            set
            {
                
                _totalLikes = value;
                RaisePropertyChanged(() => TotalLikes);
            }
        }


        public FeedModel Image1
        {
            get { return _image1; }
            set
            {
                
                _image1 = value;
                RaisePropertyChanged(() => Image1);
            }
        }

        public FeedModel Image2
        {
            get { return _image2; }
            set
            {
                
                _image2 = value;
                RaisePropertyChanged(() => Image2);
            }
        }

        public FeedModel Image3
        {
            get { return _image3; }
            set
            {
                _image3 = value;
                RaisePropertyChanged(() => Image3);
            }
        }

        public int Choice
        {
            get { return _choice; }
            set
            {
                _choice = value;
                RaisePropertyChanged(() => Choice);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand DetailsCommand1 => new SafeMvxCommand(() => Details(1));
        public IMvxCommand DetailsCommand2 => new SafeMvxCommand(() => Details(2));
        public IMvxCommand DetailsCommand3 => new SafeMvxCommand(() => Details(3));
        public IMvxCommand GoLikersCommand1 => new SafeMvxCommand(() => GoLikers(1));
        public IMvxCommand GoLikersCommand2 => new SafeMvxCommand(() => GoLikers(2));
        public IMvxCommand GoLikersCommand3 => new SafeMvxCommand(() => GoLikers(3));

        private void GoLikers(int imageNumber)
        {
            if (imageNumber == 1 && Image1.ImageSrcFeedDiscover != null && Image1.NbLikes>0)
                ShowViewModel<LikeListViewModel, LikeListParameter>(new LikeListParameter
                {
                    MediaId = Image1.PostId
                });

            if (imageNumber == 2 && Image2.ImageSrcFeedDiscover != null && Image2.NbLikes > 0)
                ShowViewModel<LikeListViewModel, LikeListParameter>(new LikeListParameter
                {
                    MediaId = Image2.PostId
                });

            if (imageNumber == 3 && Image3.ImageSrcFeedDiscover != null && Image3.NbLikes > 0)
                ShowViewModel<LikeListViewModel, LikeListParameter>(new LikeListParameter
                {
                    MediaId = Image3.PostId
                });
        }

        public void Details(int imageNumber)
        {
            if (imageNumber == 1 && Image1?.ImageSrcFeedDiscover != null)
                ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = Image1 });
            if (imageNumber == 2 && Image2?.ImageSrcFeedDiscover != null)
                ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = Image2 });
            if (imageNumber == 3 && Image3?.ImageSrcFeedDiscover != null)
                ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = Image3 });
        }


        public IMvxCommand ShowProfileCommand => new SafeMvxCommand<FeedModel>((feed) =>
        {

            //if (!this.Profile.IsRegisteredInPetPal)
            //    return;

            ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters
            {
                Feed = feed
            });
        });

        #endregion
    }
}