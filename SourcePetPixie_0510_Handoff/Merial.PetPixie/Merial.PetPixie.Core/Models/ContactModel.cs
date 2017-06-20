using System.Collections.Generic;

namespace Merial.PetPixie.Core.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
            this.Phones = new List<string>();
            this.Emails = new List<string>();
        }

        public string DisplayName { get; set; }

        public string DisplayNameAlternative { get; set; }

        public List<string> Phones { get; set; }

        public List<string> Emails { get; set; }

        public string ContactId { get; set; }

        public string PhotoUri { get; set; }
    }
}
