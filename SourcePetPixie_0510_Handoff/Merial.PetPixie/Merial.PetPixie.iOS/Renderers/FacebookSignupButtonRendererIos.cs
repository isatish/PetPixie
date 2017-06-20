using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Merial.PetPixie;
using Merial.PetPixie.iOS;
using Merial.PetPixie.Core.Views;
using Merial.PetPixie.Core;
using Facebook.LoginKit;
using Facebook.CoreKit;
using Xamarin.Auth;

[assembly: ExportRenderer(typeof(FacebookSignupButton), typeof(FacebookSignupButtonRendererIos))]
namespace Merial.PetPixie.iOS
{
    public class FacebookSignupButtonRendererIos : ButtonRenderer
    {
        #region Fields
        string[] Permissions = new string[] { "public_profile", "email" };
        private Account twitterAccount;
        #endregion

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                UIButton button = Control;

                button.TouchUpInside += delegate
                {
                   // HandleFacebookLoginClicked();
                    FacebookSignup(button);
                };
            }
        }

        public void FacebookSignup(UIButton btnFacebook)
        {
                LoginManager fbLoginManager = new LoginManager();
                fbLoginManager.LoginBehavior = LoginBehavior.Native;
                fbLoginManager.LogInWithReadPermissions(Permissions, (result, error) =>
                {
                    if (error != null)
                    {
                        new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
                        return;
                    }

                    // Handle if the user cancelled the request
                    if (result.IsCancelled)
                    {
                        new UIAlertView("The request was cancelled", "If you are using a Test App Id, please, make sure that your account have an Administrator role at:\rhttps://developers.facebook.com/apps/appid/roles/", null, "OK", null).Show();
                        return;
                    }

                    // Do your magic if the request was successful
                    App.PostSuccessFacebookSignupAction(AccessToken.CurrentAccessToken.UserID, AccessToken.CurrentAccessToken.TokenString);

                    ;
                });
        }


    }
}
