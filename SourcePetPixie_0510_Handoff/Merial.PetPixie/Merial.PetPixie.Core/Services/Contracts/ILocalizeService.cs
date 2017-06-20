using System.Globalization;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
