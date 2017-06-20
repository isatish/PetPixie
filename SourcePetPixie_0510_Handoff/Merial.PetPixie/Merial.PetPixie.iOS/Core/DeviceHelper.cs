using System;
using Contacts;
using Foundation;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Plugins;

namespace Merial.PetPixie.iOS.Core
{
	public class DeviceHelper: IDeviceHelper
	{
		public DeviceHelper()
		{
		}

		public ContactModel[] GetContacts()
		{
            try
            {

                // Define fields to be searched
                var fetchKeys = new NSString[] { CNContactKey.Identifier, CNContactKey.GivenName, CNContactKey.FamilyName };

                // Create predicate to locate requested contact
                //var predicate = CNContact.GetPredicateForContacts("Silverlight");
                //var predicate = CNContact.GetPredicateForContacts("");
                var predicate = CNContact.GetPredicateForContacts("Silverlight");




                // Grab matching contacts
                var store = new CNContactStore();
                NSError error;
                var contacts = store.GetUnifiedContacts(predicate, fetchKeys, out error);
              

                ContactModel[] ContactModels = new ContactModel[0];
                if (contacts != null)
                {
                    int totalContacts = contacts.Length;
                    ContactModels = new ContactModel[totalContacts];


                    int count = 0;
                    foreach (var contact in contacts)
                    {
                        ContactModels[count] = new ContactModel() { ContactId = contact.Identifier, DisplayName = contact.GivenName + " " + contact.FamilyName };
                        count += 1;
                    }
                }
                return ContactModels;
            }
            catch (Exception exc)
            {
                var message = exc.Message;
            }

            return new ContactModel[1];
			//throw new NotImplementedException();
		}

		public void HideKeyBoard()
		{
			
		}
	}
}

