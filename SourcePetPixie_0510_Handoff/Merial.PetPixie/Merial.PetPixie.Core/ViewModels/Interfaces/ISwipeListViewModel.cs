using System.Collections.ObjectModel;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.ViewModels.Interfaces
{
    public interface ISwipeListViewModel
    {
        ObservableCollection<EntityBase> EntityList { get; set; }

        bool CanSwipeItemAtPosition(int index);

        void ItemSwiped(int index);
    }
}
