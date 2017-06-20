using Merial.PetPixie.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public class PersonDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LargeLeftTemplate { get; set; }

        public DataTemplate LargeRightTemplate { get; set; }

        public DataTemplate AllEqualTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            DiscoverItemViewModel vm = (DiscoverItemViewModel)item;

            int image1Likes = 0;
            int image2Likes = 0;
            int image3Likes = 0;

            if (vm.Image1 != null) { image1Likes = vm.Image1.NbLikes; }
            if (vm.Image2 != null) { image2Likes = vm.Image2.NbLikes; }
            if (vm.Image3 != null) { image3Likes = vm.Image3.NbLikes; }



            if ((image1Likes == 0) && (image2Likes == 0) && (image3Likes == 0))
            {
                return AllEqualTemplate;
            }
            else if (image1Likes > image3Likes)
            {
                return LargeLeftTemplate;
            }
            else
            {
                return LargeRightTemplate;
            }


      //      if (((DiscoverItemViewModel)item).Image1.NbLikes == 0;
      //      return ((DiscoverItemViewModel)item).Choice > 1 ? LargeLeftTemplate : LargeRightTemplate;
        }
    }
}
