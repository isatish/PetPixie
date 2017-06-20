using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public class FunTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HatTemplate { get; set; }
        public DataTemplate GlassesTemplate { get; set; }
        public DataTemplate HairTemplate { get; set; }
        public DataTemplate AccessoriesTemplate { get; set; }
        public DataTemplate ClothesTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            switch ((int)item)
            {
                case 1:
                    return HatTemplate;

                case 2:
                    return GlassesTemplate;

                case 3:
                    return HairTemplate;

                case 4:
                    return AccessoriesTemplate;

                case 5:
                    return ClothesTemplate;

                default:
                    return HatTemplate;

            }


        }
    }
}
