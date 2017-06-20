using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
        public class GetStartedTemplateSelector : DataTemplateSelector
        {
                public DataTemplate FeedTemplate { get; set; }
                public DataTemplate DiscoverTemplate { get; set; }
                public DataTemplate TakeAPhotoTemplate { get; set; }
                public DataTemplate MyPackTemplate { get; set; }
                public DataTemplate VetTemplate { get; set; }

                protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
                {


                        switch ((int)item)
                        {
                                case 1:
                                        return FeedTemplate;

                                case 2:
                                        return DiscoverTemplate;

                                case 3:
                                        return TakeAPhotoTemplate;

                                case 4:
                                        return MyPackTemplate;

                                case 5:
                                        return VetTemplate;

                                default:
                                        return FeedTemplate;

                        }
                }
        }
}
