using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels {
	public class ProfileDetailViewModel : ProfileDetailBaseViewModel
    {
	    public ProfileDetailViewModel(IUserService userService, IFriendService friendService, IProfileService profileService, IPetService petService, IFeedService feedService, IBreedService breedService) 
            : base(userService, friendService, profileService, petService, feedService, breedService)
	    {
	    }
    }

}
