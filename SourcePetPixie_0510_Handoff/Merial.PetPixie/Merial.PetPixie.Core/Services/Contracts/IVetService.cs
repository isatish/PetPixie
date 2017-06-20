using Merial.PetPixie.Core.Models.Kinvey;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IVetService
    {
	    Task<List<KVet>> SearchByName(string expression);
		Task<List<KVet>> GetByLatLng(double lat, double lng, double distance,bool sortByDistance = false);
    }
}