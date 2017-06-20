using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Merial.PetPixie.Core
{
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        private int _age;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age == value) return;
                _age = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
