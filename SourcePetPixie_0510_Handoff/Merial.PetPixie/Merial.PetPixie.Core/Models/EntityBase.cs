using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Merial.PetPixie.Core.Models
{
    public class EntityBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetAndNotifyPropertyChanged<T>(ref T propertyField, T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyField!= null && propertyField.Equals(value)) return;

            propertyField = value;
            this.OnPropertyChanged(propertyName);
        }
    }
}