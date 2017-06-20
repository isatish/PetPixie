using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class VetReminderModel :EntityBase, IItemRadio
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Adresse { get; set; }

        public string PhoneNumber { get; set; }
        public string OtherValue { get; set; }

        public static VetReminderModel CreateFrom(KVet vet)
        {
            return new VetReminderModel
            {
                Id = vet.Id,
                Name = vet.Name,
                Adresse = vet.Address,
                PhoneNumber = vet.PhoneNumber
            };
        }

        public static VetReminderModel CreateFrom(string vetId, string otherValue)
        {
            if (string.IsNullOrEmpty(vetId) && string.IsNullOrEmpty(otherValue))
                return null;
            return new VetReminderModel
            {
                Id = vetId,
                OtherValue = otherValue,
            };
        }

        public string NameDisplay => string.IsNullOrEmpty(Id) ? OtherValue : Name;
    }
}
