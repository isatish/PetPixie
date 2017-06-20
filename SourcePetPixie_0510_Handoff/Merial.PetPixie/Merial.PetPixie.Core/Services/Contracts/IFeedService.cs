using Merial.PetPixie.Core.Models.Kinvey;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IFeedService
    {
        Task<List<KFeed>> GetAllAsync(int skip = 0);
        Task<List<KFeed>> GetAllDiscoverAsync(int skip = 0, int? limit =null);
        Task<List<KFeed>> GetAllDiscoverByTagAsync(string tag, int skip = 0);
        Task<List<KFeed>> GetAllDiscoverByUserAsync(string userId, int skip = 0);
        Task<KResult> Like(string postId);
        Task UnLike(string postId);
        Task<ObservableCollection<KFrom>> GetLikersByMediaAsync(string postId, int skip = 0);
		Task<List<KFeed>> GetFeedByUserAsync(string profileId, string petId = null, int skip = 0);
        Task<bool> RemoveFeed(string id);
    }
}