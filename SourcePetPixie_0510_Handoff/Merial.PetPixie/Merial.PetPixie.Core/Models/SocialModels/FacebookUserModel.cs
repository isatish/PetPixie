namespace Merial.PetPixie.Core.Models
{
    public class FacebookUserModel : SocialUserModel
    {
        #region Properties

        public string UserId { get; set; }

        public string Token { get; set; }

        #endregion
    }
}