using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core.Services
{
    public class CommentService : KinveyServiceBase, ICommentService
    {
        public CommentService(IKinveyService kinveyService) : base(kinveyService)
        {
        }

        public async Task<List<KComment>> Get(string postId)
        {
            var comments = await Execute<List<KComment>>($"getComments{postId}",()=>PostRPCAsync<List<KComment>>("GetComments", new { media_id = postId }),LocalCachingPolicy.NoPersistence,RequestErrorHandlingPolicy.ThrowErrorOnStack);
            return comments;
        }

        public async Task<KComment> Add(string postId, string text)
        {
            var comment = await Execute($"add{postId}", ()=>PostRPCAsync<KComment>("AddComment", new { media_id = postId, text = text }),LocalCachingPolicy.NoPersistence,RequestErrorHandlingPolicy.SilentErrorReturnNull);
            return comment;
        }

        public async Task Delete(string postId, string commentId)
        {
            await Execute("delete",()=>PostRPCAsync<object>("DeleteComment", new { media_id = postId, comment_id = commentId }),LocalCachingPolicy.NoPersistence);
        }
    }
}