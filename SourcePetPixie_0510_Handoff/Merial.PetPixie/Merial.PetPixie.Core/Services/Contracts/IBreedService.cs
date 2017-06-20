using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public  interface IBreedService
    {
        List<KBreed> GetListBreedName(string[] value);
        Task<List<KBreed>> GetBreeds();
    }
}