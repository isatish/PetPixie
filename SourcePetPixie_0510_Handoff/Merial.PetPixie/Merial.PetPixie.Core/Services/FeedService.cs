using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Merial.PetPixie.Core.Services
{
	public class FeedService : KinveyServiceBase, IFeedService
    {
		private const int Limit = 27;
		private const int LimitLiker = 50;

		public FeedService(IKinveyService kinveyService) : base(kinveyService)
        {
		}

		public async Task<List<KFeed>> GetAllAsync(int skip = 0)
        {
			List<KFeed> allFeed = new List<KFeed>();
			try {
				allFeed = await base.PostRPCAsync<List<KFeed>>("Feed", new { access = Constants.FeedAccess.Feed, limit = Limit, skip });
            }
			catch (Exception e) {
                _logger.Error(e);
				Debug.WriteLine(e.Message);
			}
			return allFeed;
		}

		public async Task<List<KFeed>> GetAllDiscoverAsync(int skip = 0, int? limit = null)
        {
		    
			List<KFeed> allFeed = new List<KFeed>();
		    var coef = 1;
		    if (skip > 0)
		        coef = 2;

            if (limit == null)
            {
                limit = Limit * coef;

            }
            try {
				allFeed = await base.PostRPCAsync<List<KFeed>>("Feed", new { access = Constants.FeedAccess.Discover, limit = limit, skip });
			}
			catch (Exception e) {
                _logger.Error(e);
				Debug.WriteLine(e.Message);
			}
			return allFeed;
		}

		public async Task<List<KFeed>> GetAllDiscoverByTagAsync(string tag, int skip = 0)
        {
			List<KFeed> allFeed = new List<KFeed>();
			try {
				allFeed = await base.PostRPCAsync<List<KFeed>>("Feed", new { access = Constants.FeedAccess.Discover, limit = Limit, skip, tag });
			}
			catch (Exception e) {
                _logger.Error(e);
				Debug.WriteLine(e.Message);
			}
			return allFeed;
		}

		public async Task<List<KFeed>> GetAllDiscoverByUserAsync(string userId, int skip = 0)
        {
			List<KFeed> allFeed = new List<KFeed>();
			try {
				allFeed = await base.PostRPCAsync<List<KFeed>>("Feed", new { access = Constants.FeedAccess.Profile, limit = Limit, skip, profile_id = userId });
			}
			catch (Exception e) {
                _logger.Error(e);
				Debug.WriteLine(e.Message);
			}
			return allFeed;
		}

		public async Task<KResult> Like(string postId)
        {
			var like = await base.PostRPCAsync<KResult>("AddLike", new { media_id = postId });
            return like;
		}

		public async Task UnLike(string postId)
        {
			await base.PostRPCAsync<object>("RemoveLike", new { media_id = postId });
		}

		public async Task<ObservableCollection<KFrom>> GetLikersByMediaAsync(string postId, int skip = 0)
        {
			var liker = await base.PostRPCAsync<ObservableCollection<KLike>>("GetLikes", new { media_id = postId, limit = LimitLiker, skip, });
            return new ObservableCollection<KFrom>(liker.Select(l => l.From).ToList());
		}

		private readonly System.Collections.Concurrent.ConcurrentDictionary<FeedByUserAsync, FeedByUserAsync> _feedByUserAsync = new ConcurrentDictionary<FeedByUserAsync, FeedByUserAsync>();

		public Task<List<KFeed>> GetFeedByUserAsync(string profileId, string petId = null, int skip = 0)
        {
			var feedByUserAsync = new FeedByUserAsync(profileId, petId, skip);
			FeedByUserAsync feedByUserAsyncResult;
			if (_feedByUserAsync.TryGetValue(feedByUserAsync, out feedByUserAsyncResult)) {
				return feedByUserAsyncResult.Task;
			}
			var task = petId == null
				? PostRPCAsync<List<KFeed>>("Feed",
					new { access = Constants.FeedAccess.Profile, limit = Limit, skip, profile_id = profileId })
				: PostRPCAsync<List<KFeed>>("Feed",
					new { access = Constants.FeedAccess.Profile, limit = Limit, skip, profile_id = profileId, pet_id = petId });
			task.ContinueWith((result) => {
				_feedByUserAsync.TryRemove(feedByUserAsync, out feedByUserAsyncResult);
			});
			feedByUserAsync.Task = task;
			_feedByUserAsync.TryAdd(feedByUserAsync, feedByUserAsync);
			return task;
		}

	    public async Task<bool> RemoveFeed(string id)
	    {
            try
            {
                var response = await DeleteAppdataAsync<KMedia>("Media", id);
                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
                return false;
            }
        }

	    #region private class
		private class FeedByUserAsync
        {
			public string ProfileId { get; set; }
			public string PetId { get; set; }
			public int Skip { get; set; }
			public Task<List<KFeed>> Task { get; set; }

			public FeedByUserAsync(string profileId, string petId, int skip)
            {
				ProfileId = profileId;
				PetId = petId;
				Skip = skip;
			}

			protected bool Equals(FeedByUserAsync other) {
				return string.Equals(ProfileId, other.ProfileId) && string.Equals(PetId, other.PetId) && Skip == other.Skip;
			}

			public override bool Equals(object obj) {
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((FeedByUserAsync) obj);
			}

			public override int GetHashCode() {
				unchecked {
					var hashCode = (ProfileId != null ? ProfileId.GetHashCode() : 0);
					hashCode = (hashCode*397) ^ (PetId != null ? PetId.GetHashCode() : 0);
					hashCode = (hashCode*397) ^ Skip;
					return hashCode;
				}
			}
        }
		#endregion
	}
}