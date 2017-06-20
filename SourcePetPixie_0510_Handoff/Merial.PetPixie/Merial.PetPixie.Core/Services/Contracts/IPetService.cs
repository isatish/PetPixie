using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
	public interface IPetService
    {
		Task<IEnumerable<KPetWithReminders>> GetAllAsync(KProfile profile, bool withDetails = false);
	    Task<bool> Save(PetModel pet);
	    Task<bool> RemovePet(PetModel pet);
	}
}