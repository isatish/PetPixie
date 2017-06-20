using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IProfileService
    {
        Task<KProfile> GetProfileById(string profileId, bool isAuthentification = false);
        Task<KProfile> EditProfile(KProfile editedProfile, bool forceSaveLocal =false);
	    Task DeleteAccountAsync();
	    Task<KInappropriateReport> ReportUser(string userId);
	    Task<KUserRelationship> BlockUser(string shownProfileId);
	    Task<bool> IsUserBlocked(string userId);
		Task<bool> UnblockUser(string userId);
        Task<KProfile> ReloadProfile(string profileId = null);
        Task<List<KProfile>> GetProfileBySearchText(string searchText);
	    Task<KVetRating> RateVet(string vetId, int rate);
		Task RemoveRateVet(string vetId);
	    Task<int> GetPostCountByPet(string petId, string profileId);
        event EventHandler<ProfilChangeArgs> OnProfileChangeEvent;
    }
}