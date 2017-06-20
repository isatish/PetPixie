using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels.Core
{
    public interface IAuthenticationViewModel
    {
        bool IsFacebookLoading { get; set; }

        bool IsTwitterLoading { get; set; }

        IMvxCommand LoginWithFacebookCommand { get;  }

        IMvxCommand LoginWithTwitterCommand { get; }
    }
}