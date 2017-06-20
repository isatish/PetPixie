using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IReminderService
    {
        Task Delete(string petId, ReminderModel reminder);
        Task<List<ReminderModel>> Get(string petId);
        Task<ReminderModel> GetById(string reminderId);
        Task<List<ReminderTypeModel>> GetAllReminderType();
        Task<List<ProductModel>> GetProducts(ReminderType type);

        Task<List<ReminderFrequencyModel>> GetRecurrence(); 
        Task Save(ReminderModel reminder,string petId);

        Task<List<KReminder>> GetAllReminder(string profileId = null );
    }
}
