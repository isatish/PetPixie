using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services
{
    public class VetService : KinveyServiceBase, IVetService
    {
        public VetService(IKinveyService kinveyService) : base(kinveyService)
        {
        }

        //public async Task<List<KVet>> GetAllAsync()
        //{
        //    List<KVet> allVets = new List<KVet>();
        //    try
        //    {
        //        JsonObject ob = new JsonObject
        //        {
        //            //new KeyValuePair<string, object>("$nearSphere", new[] {45.76, 4.8}),
        //            new KeyValuePair<string, object>("$nearSphere", new[] { -122.00, 37.35}),
        //            new KeyValuePair<string, object>("$maxDistance", "10000")
        //        };
        //        JsonObject ob1 = new JsonObject { new KeyValuePair<string, object>("_geoloc", ob) };
        //        allVets = await base.GetAppDataAsync<KVet>("Vet", ob1);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }
        //    return allVets;
        //}

		public async Task<List<KVet>> SearchByName(string expression)
        {
	        try {
				var regex = $"{{\"name\":{{\"$regex\":\"^.*(?i){expression}.*\"}}}}";
				return await GetAppDataAsync<KVet>("Vet", regex);
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
			}
			return new List<KVet>();
		}

		public async Task<List<KVet>> GetByLatLng(double lat, double lng, double distance = 50.0,bool sortByDistance = false)
        {
			List<KVet> vets = null;
			try {
				JsonObject ob = new JsonObject
				{
                    //new KeyValuePair<string, object>("$nearSphere", new[] {45.76, 4.8}),
                    new KeyValuePair<string, object>("$nearSphere", new[] { lng, lat}),
					new KeyValuePair<string, object>("$maxDistance", distance)
				};
				JsonObject ob1 = new JsonObject { new KeyValuePair<string, object>("_geoloc", ob) };
			    if (sortByDistance == true)
			    {
			        var query = ob1.ToString();
                    query += "&sort={\"distance\":1}";
                    vets = await GetAppDataAsync<KVet>("Vet", query);
                }
                else
				    vets = await GetAppDataAsync<KVet>("Vet", ob1);
			}
			catch (Exception e)
            {
                _logger.Error(e);
                Debug.WriteLine(e.Message);
			}
			return vets ?? new List<KVet>();
		}
	}
}