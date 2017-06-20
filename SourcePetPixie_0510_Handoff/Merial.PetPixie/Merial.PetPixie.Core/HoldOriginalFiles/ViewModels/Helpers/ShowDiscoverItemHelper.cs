using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels.Helpers
{
    public class ShowDiscoverItemHelper : ViewModelBase
    {
        public ShowDiscoverItemHelper(string text)
        {
            text = text.Substring(1); // Remove the # char

            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = text,
                TitlePage = "#"+ text
            });
        }
    }
}
