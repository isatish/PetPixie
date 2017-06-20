namespace Merial.PetPixie.Core.Models
{
    public class TwitterUserModel : SocialUserModel
    {
        #region Properties

        public string UserId { get; set; }
        public string OauthToken { get; set; }
        public string OauthTokenSecret { get; set; }

        #endregion
    }
}
