using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class LeftNavViewModel : ViewModelBase
    {


        #region Fields

       // private ObservableCollection<VetModel> _vets;
      //  private bool _redirect;
      //  private bool _isMenuVisible = false;
      //  private ProfileModel _profile;


        #endregion



        #region Properties


        //public ProfileModel Profile
        //{
        //    get
        //    {

        //        if (_profile == null)
        //        {
        //            var profile = Settings.CurrentUserProfile;

        //            _profile = ProfileModel.CreateFrom(profile, _profile?.FileMetaData);
        //        }



        //        return _profile;
        //    }
        //    set
        //    {
        //        _profile = value;
        //        RaisePropertyChanged(() => Profile);
              
        //    }
        //}


        public string ProfilePictureUrl
        {
            get
            {
             
              return Profile.ImageSrc;

            }
        }


        public KImage ProfilePicture
        {
            get

            {
                return Settings.CurrentUserProfile.ProfilePicture;
            }

        }


        public string UserFullName
        {
            get

            {
                return Settings.CurrentUserProfile.FullName;
            }

        }


        public string UserBiography
        {
            get

            {
                return Settings.CurrentUserProfile.Biography;
            }

        }

        public string UserEmail
        {
            get

            {
                return Settings.CurrentUserProfile.Email;
            }

        }







        //public ObservableCollection<PetModel> UserPets
        //{
        //    get
        //    {
        //        return Profile.Pets;
        //    }

        //}








        //public bool IsMenuVisibile
        //{
        //    get { return _isMenuVisible; }
        //    set
        //    {
        //        _isMenuVisible = value;
        //        RaisePropertyChanged(() => IsMenuVisibile);
        //    }
        //}
        #endregion

        #region Commands

        //public IMvxCommand ShowAboutCommand => new SafeMvxCommand(DoShowAboutCommand);
        //private void DoShowAboutCommand()
        //{
        //    IsMenuVisibile = !IsMenuVisibile;
        //}
 
        #endregion


        #region Actions
        //private void OnFindVets()
        //{
        //    ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters { SelectedVet = null });
        //}
        //private void OnVetSelected(VetModel vet)
        //{
        //    ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters
        //    {
        //        SelectedVet = Settings.CurrentUserProfile.Vets.FirstOrDefault(v => v.Id.Equals(vet.Id))
        //    });
        //}
        #endregion


        //protected override Task<bool> LoadDataAsync()
        //{

        //    return base.LoadDataAsync();
        //}

        //public void Init(bool redirect)
        //{
        //    _redirect = redirect;

        //    //How does the current app trigger the load data
        //    //  if (redirect == false)
        //    {
        //        LoadDataAsync();

        //    }
        //}
    }
}