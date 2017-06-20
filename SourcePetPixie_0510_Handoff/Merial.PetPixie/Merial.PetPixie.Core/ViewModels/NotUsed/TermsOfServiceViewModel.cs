namespace Merial.PetPixie.Core.ViewModels
{
    public class TermsOfServiceViewModel : BaseViewModel
    {
        
        private string _someTitle = "Terms of Service title";
        public string SomeTitle { 
            get
            {
                return _someTitle;
            }
            set
            {
                _someTitle = value;
            }
        }
    }
}
