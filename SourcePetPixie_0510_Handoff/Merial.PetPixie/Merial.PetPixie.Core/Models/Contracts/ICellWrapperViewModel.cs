using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Models.Contracts
{
    public interface ICellWrapperViewModel
    {
        object Item { get; }
        object ViewModel { get; }
    }
}
