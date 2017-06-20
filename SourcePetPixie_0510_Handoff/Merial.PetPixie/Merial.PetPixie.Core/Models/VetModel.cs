using Merial.PetPixie.Core.Models.Kinvey;
using System.Collections.Generic;
using Merial.PetPixie.Core.Models.Interface;

namespace Merial.PetPixie.Core.Models
{
    public class VetModel : EntityBase
    {
        private string _id;
        private KAcl _acl;
        private KKmd _kmd;
        private string _name;
        private IList<double> _geoLoc;
        private string _address;

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public KAcl Acl
        {
            get { return _acl; }
            set
            {
                if (value == _acl) return;
                _acl = value;
                OnPropertyChanged();
            }
        }

        public KKmd Kmd
        {
            get { return _kmd; }
            set
            {
                if (value == _kmd) return;
                _kmd = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

       

        public IList<double> GeoLoc
        {
            get { return _geoLoc; }
            set
            {
                if (value.Equals(_geoLoc)) return;
                _geoLoc = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public static VetModel CreateFrom(KVet vet)
        {
            return new VetModel
            {
                Id = vet.Id,
                Acl = vet.Acl,
                Kmd = vet.Kmd,
                Name = vet.Name,
                GeoLoc = vet.Geoloc,
                Address = vet.Address
            };
        }
    }
}