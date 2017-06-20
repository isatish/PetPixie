using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public class FriendTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OpeningTemplate { get; set; }
        public DataTemplate JoinTemplate { get; set; }
        public DataTemplate ReminderTemplate { get; set; }
        public DataTemplate VetTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

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
