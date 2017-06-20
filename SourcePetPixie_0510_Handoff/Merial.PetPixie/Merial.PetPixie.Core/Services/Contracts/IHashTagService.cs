using System.Collections.Generic;
using System.Threading.Tasks;
using KHashTag = Merial.PetPixie.Core.Models.Kinvey.KHashTag;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IHashTagService
    {
        Task<List<KHashTag>> GetAllAsync();

        Task<List<KHashTag>> GetTagByTextAsync(string text);
    }
}