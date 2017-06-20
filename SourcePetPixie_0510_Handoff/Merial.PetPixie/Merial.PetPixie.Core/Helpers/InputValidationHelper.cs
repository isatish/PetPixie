using System.Text.RegularExpressions;

namespace Merial.PetPixie.Core.Helpers {
    public class InputValidationHelper {

        public static bool ValidateUserName(string username)
        {
            Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]*$");
            bool sts = objAlphaPattern.IsMatch(username); ;
            return sts;
        }
        public static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }
}