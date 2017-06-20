using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Interface;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class ReminderTypeModel :EntityBase, IItemRadio
    {

        public string Name { get; set; }

        public ReminderType Type { get; set; }

        public ReminderSubType SubType { get; set; }

        public string  OtherValue { get; set; }

        public int Id { get; set; }

        public List<ProductModel> Products { get; set; } 

        public string NameDisplay => Type == ReminderType.Other ? OtherValue : Name;

        public static ReminderTypeModel CreateFrom(KReminderType remindeType)
        {
            return GenerateFromId(remindeType.Id, remindeType.Name, remindeType.Products);
        }

        private static ReminderTypeModel GenerateFromId(string id, string name, KProduct[] products)
        {
            switch (id)
            {
                case "3":
                    return new ReminderTypeModel
                    {
                        Id = int.Parse(id),
                        Name = name,
                        Type = ReminderType.DentalTreatment,
                        SubType = ReminderSubType.Product,
                        Products = products?.Where(x => !x.Name.Contains("Cat")).Select(ProductModel.CreateFrom).ToList()
                    };

                case "1":
                    return new ReminderTypeModel
                    {
                        Id = int.Parse(id),
                        Name = name,
                        Type = ReminderType.FleaTickTreatment,
                        SubType = ReminderSubType.Product,
                        Products = products?.Where(x => !x.Name.Contains("Cat")).Select(ProductModel.CreateFrom).ToList()
                    };
                case "2":
                    return new ReminderTypeModel
                    {
                        Id = int.Parse(id),
                        Name = name,
                        Type = ReminderType.HeartwormTreatment,
                        SubType = ReminderSubType.Product,
						Products = products?.Where(x=>!x.Name.Contains("Cat")).Select(ProductModel.CreateFrom).ToList()
                    };
                case "4":
                    return new ReminderTypeModel
                    {
                        Id = int.Parse(id),
                        Name = name,
                        Type = ReminderType.VetVisit,
                        SubType = ReminderSubType.Other
                    };
                default:
                    return default(ReminderTypeModel);
            }
        }

        public static ReminderTypeModel CreateFrom(ReminderType reminderType,string name)
        {
            
            if (reminderType == ReminderType.Other)
            {
               return  new ReminderTypeModel
                {
                    Name = "Other",
                    Type = ReminderType.Other,
                    SubType = ReminderSubType.Other,
                    OtherValue = name
                   
                };
            }
            return GenerateFromId(((int)reminderType).ToString(), name, null);



        }
    }
}
