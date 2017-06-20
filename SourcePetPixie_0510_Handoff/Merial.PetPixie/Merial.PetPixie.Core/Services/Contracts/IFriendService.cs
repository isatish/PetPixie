using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IFriendService
    {
        Task<List<KProfile>> FindFriendsFromMailsOrPhoneNumber(string[] emails, string[] phoneNumbers);

        Task<List<KProfile>> FindFriendsByFacebookId(string[] facebookIds);
        
        Task<IEnumerable<KProfile>> GetFollowingOf(string followerProfileId);

        Task<IEnumerable<KProfile>> GetFollowersOf(string followedProfileId);

        Task<KUserRelationship> FollowProfile(string followerProfileId, string followedProfileId);

        Task<bool> UnfollowProfile(string followerProfileId, string followedProfileId);
    }
}