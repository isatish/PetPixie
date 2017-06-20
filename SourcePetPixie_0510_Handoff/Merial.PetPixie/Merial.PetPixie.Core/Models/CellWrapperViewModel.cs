using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Contracts;

namespace Merial.PetPixie.Core.Models
{
    public class CellWrapperViewModel<T, TV> : ICellWrapperViewModel
    {
        public T Item { get; set; }

        public TV ViewModel { get; set; }

        object ICellWrapperViewModel.Item => Item;

        object ICellWrapperViewModel.ViewModel => ViewModel;

        public CellWrapperViewModel(T item, TV viewModel)
        {
            Item = item;
            ViewModel = viewModel;
        }
    }

   
}
