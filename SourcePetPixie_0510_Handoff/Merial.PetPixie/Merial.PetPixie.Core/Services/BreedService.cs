using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.Core.Services
{
    public class BreedService : KinveyServiceBase, IBreedService
    {
        public List<KBreed> Breeds { get; private set; }

        private const string Endpoint = "Breed";

        public BreedService(IKinveyService kinveyService) : base(kinveyService)
        {
            //dsmvx GetBreeds();
        }

        private async Task<List<KBreed>> GetAll()
        {
            try
            {
                return await GetAppDataAsync<KBreed>(Endpoint);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
            }
            return new List<KBreed>();
        }

        public List<KBreed> GetListBreedName(string[] value)
        {
            if (Breeds == null || value == null)
                return null;

            return Breeds.Where(b => value.Contains(b.Id)).ToList();
        }

        public async Task<List<KBreed>> GetBreeds()
        {
            if (Breeds == null || !Breeds.Any())
                Breeds = await GetAll();

            return Breeds;
        }
    }
}