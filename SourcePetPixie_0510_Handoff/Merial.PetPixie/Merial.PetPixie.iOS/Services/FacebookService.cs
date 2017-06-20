using System;
using System.Threading.Tasks;
using Facebook.CoreKit;
//using Facebook.CoreKit;
using Foundation;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Newtonsoft.Json;

namespace Merial.PetPixie.iOS.Services
{
    public class FacebookService : IFacebookService
    {
        public event EventHandler<FacebookUserModel> DataResponse;

		//public void GetFacebookData()
		//{
  //          GraphRequest request = new GraphRequest();
		//}

		    public void GetFacebookData()
		{
			//GraphRequest request = GraphRequest..NewMeRequest(AccessToken.CurrentAccessToken, this);
			////Bundle parameters = new Bundle();
			//parameters.PutString("fields", "id,name,email");
			//request.Parameters = parameters;
			//request.ExecuteAsync();

            var graphRequest = new GraphRequest("/me?fields=id,name,gender", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
            var requestConnection = new GraphRequestConnection();
            requestConnection.AddRequest(graphRequest, (connection, result, error) =>
              {
                  var profile = result;
              });
            requestConnection.Start();
		}

//		public void OnCompleted(GraphResponse p1)
//		{
//			var email = string.Empty;

//			if (!jsonObject.IsNull("email"))
//				email = jsonObject.Get("email").ToString();

//            DataResponse?.Invoke(this, new FacebookUserModel
//            {
//                Email = email,
//                UserName = Profile.CurrentProfile?.Name,
//                FullName = Profile.CurrentProfile?.Name,

//                //ImageUrl = Profile.CurrentProfile?.GetProfilePictureUri(400, 400)?.ToString()
//
//                ImageUrl = Profile.CurrentProfile.ImageUrl(ProfilePictureMode.Normal, new CoreGraphics.CGSize( 400, 400)).StandardizedUrl.ToString()//?.GetProfilePictureUri(400, 400)?.ToString()
//			});
//		}

	}
}