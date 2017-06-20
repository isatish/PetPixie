using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Platform;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;

namespace Merial.PetPixie.iOS.Services
{
    public class TwitterService : ITwitterService
    {
        private Account twitterAccount;

        public event EventHandler<TwitterUserModel> DataResponse;

        public async void GetTwitterData()
        {
            if (twitterAccount == null)
                this.InitTwitter();

            var request = new OAuth1Request(
                "GET",
                new Uri("https://api.twitter.com/1.1/account/verify_credentials.json "),
                null,
                this.twitterAccount);

            await request.GetResponseAsync().ContinueWith(t =>
            {
                var res = t.Result;
                var resString = res.GetResponseText();

                var parsedTwitterObject = JToken.Parse(resString);

                var twitterUser = new TwitterUserModel
                {
                    FullName = parsedTwitterObject["name"]?.ToString() ?? string.Empty,
                    UserName = parsedTwitterObject["screen_name"]?.ToString() ?? string.Empty,
                    ImageUrl = parsedTwitterObject["profile_image_url"]?.ToString() ?? string.Empty,
                    Email = string.Empty
                };

                this.DataResponse?.Invoke(this, twitterUser);
            });
        }

        private void InitTwitter()
        {
            IEnumerable<Account> accounts = null;
            try
            {
                accounts = AccountStore.Create().FindAccountsForService("Twitter");
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (accounts != null && accounts.Any())
                    this.twitterAccount = accounts.First();
            }
        }
    }
}