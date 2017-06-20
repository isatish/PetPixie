using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public class CarouselTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OpeningTemplate { get; set; }
        public DataTemplate JoinTemplate { get; set; }
        public DataTemplate ReminderTemplate { get; set; }
        public DataTemplate VetTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
// <local:CarouselTemplateSelector x:Key="carouselDataTemplateSelector" FeedTemplate="{StaticResource FeedTemplate}" DiscoverTemplate="{StaticResource DiscoverTemplate}"     TakeAPhotoTemplate="{StaticResource TakeAPhotoTemplate}"  MyPackTemplate="{StaticResource MyPackTemplate}"  VetTemplate="{StaticResource VetTemplate}" />

            switch ((int)item)
            {
                case 1:
                    return OpeningTemplate;

                case 2:
                    return JoinTemplate;

                case 3:
                    return ReminderTemplate;

                case 4:
                    return VetTemplate;

                default:
                    return OpeningTemplate;

            }


        }
    }
}
