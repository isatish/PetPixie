using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KPetWithReminders: KPetBase
    {
       

        public List<KReminder> Reminders { get; set; }

         public List<ReminderModel> ReminderModels { get; set; }  //ds

        public static KPetWithReminders CreateFrom(KPetBase p)
        {
            return new KPetWithReminders
            {
                Type = p.Type,
                Id = p.Id,
                Name = p.Name,
                Acl = p.Acl,
                Birthday = p.Birthday,
                Breed_ids = p.Breed_ids,
                Breeds_ids = p.Breeds_ids,
                ExpandedImages = p.ExpandedImages,
                KImage = p.KImage,
                Kdm = p.Kdm,
                OwnerProfileId = p.OwnerProfileId,
                Picture = p.Picture,
                Pictures = p.Pictures,
                VetIds = p.VetIds

            };
        }
    }
}