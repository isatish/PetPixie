using Merial.PetPixie.Core.Models.Kinvey;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface ICommentService
    {
        Task<KComment> Add(string postId, string text);
        Task Delete(string postId, string commentId);
        Task<List<KComment>> Get(string postId);
    }
}