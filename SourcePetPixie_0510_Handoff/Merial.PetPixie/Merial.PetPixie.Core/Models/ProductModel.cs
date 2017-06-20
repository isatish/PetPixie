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
    public class ProductModel : IItemRadio
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string  OtherValue { get; set; }

        public ReminderSubType Type { get; set; }

        public string Information { get; set; }

        public bool IsOtherType => Type == ReminderSubType.Other;

        public string NameDisplay => Type == ReminderSubType.Other ? OtherValue : Name;

        public static ProductModel CreateFrom(KProduct kproduct)
        {
            return new ProductModel
            {
                Id = kproduct.Id,
                Type = ReminderSubType.Product,
                Name = kproduct.Name,
                Information = kproduct.Information
            };
        }

        public static ProductModel CreateFrom(string productId, string otherProductName)
        {
            if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(otherProductName))
                return null;
            return new ProductModel
            {
                Id = productId,
                OtherValue = otherProductName,
                Type = !string.IsNullOrEmpty(productId) ? ReminderSubType.Product : ReminderSubType.Other

            };
        }
    }
}
