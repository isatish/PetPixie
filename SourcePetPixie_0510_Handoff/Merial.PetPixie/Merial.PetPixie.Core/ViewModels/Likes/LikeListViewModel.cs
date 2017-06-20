using System.Collections.ObjectModel;
using System.Diagnostics;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class LikeListViewModel : ViewModelBase<LikeListParameter>
    {

        #region Private Properties
        private ObservableCollection<KFrom> _likers;

        private readonly IFeedService _feedService;

        private string _mediaId= "56c0297990f7911200c16a53";

        #endregion

        public LikeListViewModel(IFeedService feedService)
        {
            _feedService = feedService;
        }

        #region Public properties

       
        public ObservableCollection<KFrom> Likers
        {
            get { return _likers; }
            set
            {
                _likers = value;
                RaisePropertyChanged(() => Likers);
            }
        }
        #endregion

        #region Commands

        public IMvxCommand OnLickerClickCommand => new SafeMvxCommand<KFrom>(OnLickerClickCore);

        public IMvxCommand LoadMoreCommand => new SafeMvxCommandAsync(LoadMoreData);

        public IMvxCommand ReloadCommand => new SafeMvxCommandAsync(LoadData);
        
        #endregion

        #region LifeCycle

        public override void Start()
        {
            base.Start();

           ReloadCommand.Execute();
        }
        
        #endregion

        #region Private Methods

        private async Task LoadData()
        {
            IsFetchingData = true;
            var likers = await _feedService.GetLikersByMediaAsync(_mediaId);
           
            if (likers == null || !likers.Any())
            {
                IsFetchingData = false;
                return;
            }
            Likers = likers;
            IsFetchingData = false;
        }

        private async Task LoadMoreData()
        {
            int skip = Likers.Count;
            var likers = await _feedService.GetLikersByMediaAsync(_mediaId,skip);
          
            if (likers == null || !likers.Any())
            {
                return;
            }
                
            for (int i = 0; i < likers.Count - 1; i++)
            {
                Likers.Add((likers[i]));
            }
        }

        private void OnLickerClickCore(KFrom user)
        {
            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            {
                ProfileId = user.Id
            });
        }
        #endregion

        protected override void RealInit(LikeListParameter parameter)
        {
            _mediaId = parameter.MediaId;
        }
    }

    public class LikeListParameter
    {
        public string MediaId { get; set; }
    }
}
