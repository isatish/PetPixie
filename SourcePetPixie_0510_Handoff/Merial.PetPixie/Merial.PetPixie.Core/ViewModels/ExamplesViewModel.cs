using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class ExamplesViewModel
        : MvxViewModel
    {
        public RecyclerViewModel Recycler { get; private set; }

        public ExamplesViewModel() {
            Recycler = new RecyclerViewModel();
        }
    }
}