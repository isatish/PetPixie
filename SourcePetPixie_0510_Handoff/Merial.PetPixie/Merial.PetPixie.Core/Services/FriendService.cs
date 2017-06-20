using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.Core.Services
{
    public class FriendService : KinveyServiceBase, IFriendService
    {
        public FriendService(IKinveyService kinveyService) : base(kinveyService)
        {
        }

        public async Task<List<KProfile>> FindFriendsFromMailsOrPhoneNumber(string[] emails, string[] phoneNumbers)
        {
            var foundUsers = new List<KProfile>();
            try
            {
                foundUsers = await base.PostRPCAsync<List<KProfile>>("FindFriends", new { emails, phone_numbers = phoneNumbers });
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return foundUsers;
        }

        public async Task<List<KProfile>> FindFriendsByFacebookId(string[] facebookIds)
        {
            var foundUsers = new List<KProfile>();
            try
            {
                foundUsers = await base.PostRPCAsync<List<KProfile>>("FindFriends", new { facebook_ids = facebookIds });
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return foundUsers;
        }

        public async Task<IEnumerable<KProfile>> GetFollowingOf(string followerProfileId)
        {
            var foundUsers = Enumerable.Empty<KProfile>();
            try
            {
                var result = await base.GetAppDataAsync<KUserRelationship>("UserRelationship", new { from_profile_id = followerProfileId, type = KUserRelationship.TYPE_FOLLOW } );
                foundUsers = result.Select(relationship => relationship.ToProfile).ToList();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return foundUsers;
        }

        public async Task<IEnumerable<KProfile>> GetFollowersOf(string followedProfileId)
        {
            var foundUsers = Enumerable.Empty<KProfile>();
            try
            {
                var result = await base.GetAppDataAsync<KUserRelationship>("UserRelationship", new { to_profile_id = followedProfileId, type = KUserRelationship.TYPE_FOLLOW });
                foundUsers = result.Select(relationship => relationship.FromProfile).ToList();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return foundUsers;
        }

        public async Task<KUserRelationship> GetFollowRelationship(string followerProfileId, string followedProfileId)
        {
            try
            {
                var result = 
                    await base.GetAppDataAsync<KUserRelationship>(
                        "UserRelationship", 
                        new
                        {
                            to_profile_id   = followedProfileId,
                            from_profile_id = followerProfileId,
                            type            = KUserRelationship.TYPE_FOLLOW
                        });
                var relation = result.Single();
                return relation;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw e;
            }
        }

        public async Task<KUserRelationship> FollowProfile(string followerProfileId, string followedProfileId)
        {
            try
            {
                var result = 
                    await base.SaveAppdataAsync(
                        "UserRelationship",
                        new KUserRelationship
                        {
                            FromProfileId = followerProfileId,
                            ToProfileId = followedProfileId,
                            Type = KUserRelationship.TYPE_FOLLOW
                        });

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> UnfollowProfile(string followerProfileId, string followedProfileId)
        {
            try
            {
                var relation = await GetFollowRelationship(followerProfileId, followedProfileId);
                await base.DeleteAppdataAsync<KUserRelationship>(
                    "UserRelationship",
                    relation.Id);

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                throw e;
            }
        }
    }
}