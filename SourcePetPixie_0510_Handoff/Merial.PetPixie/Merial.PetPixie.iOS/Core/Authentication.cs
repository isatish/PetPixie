using System.Collections.Generic;
using System.Linq;
//using Facebook.CoreKit;
using Merial.PetPixie.Core;
using Xamarin.Auth;


namespace Merial.PetPixie.iOS.Core
{
    public class Authentication: IAuthentication
    {
        public bool IsAuthenticated
        {
            get
            {
      //ds          if (AccessToken.CurrentAccessToken != null && Profile.CurrentProfile != null)
      //ds          {
      //ds              return true;
      //ds          }
                
                IEnumerable<Account> accounts = AccountStore.Create().FindAccountsForService("Twitter");
                return accounts.Any();
            }
        }
    }
}