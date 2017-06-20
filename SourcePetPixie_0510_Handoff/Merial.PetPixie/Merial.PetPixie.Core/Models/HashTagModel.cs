using Merial.PetPixie.Core.Models.Kinvey;
using KHashTag = Merial.PetPixie.Core.Models.Kinvey.KHashTag;

namespace Merial.PetPixie.Core.Models
{
    public class HashTagModel : EntityBase
    {
        private string _id;
        private KAcl _acl;
        private KKmd _kmd;
        private string _text;
        private int _mediaCount;

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

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        public int MediaCount
        {
            get { return _mediaCount; }
            set
            {
                if (value == _mediaCount) return;
                _mediaCount = value;
                OnPropertyChanged();
            }
        }

        public static HashTagModel CreateFrom(KHashTag hashTag)
        {
            return new HashTagModel
            {
                Id = hashTag.Id,
                Acl = hashTag.Acl,
                Kmd = hashTag.Kmd,
                Text = hashTag.Text,
                MediaCount = hashTag.MediaCount
            };
        }
    }
}