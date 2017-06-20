using System.Diagnostics;
using System.Globalization;
//using Merial.PetPixie.Core.Services.Contracts;

namespace XamForms.MvxTemplate.Droid.Services
{
    public class LocalizeService // : Merial.PetPixie.Core.Services.Contracts.ILocalizeService
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-"); // turns pt_BR into pt-BR
            try {
                return new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e)
            {
                Debug.WriteLine(e.Message);
            }

            return new CultureInfo("en");
        }
    }
}