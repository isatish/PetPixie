namespace Merial.PetPixie.Core.Models
{
	public class UserRatingModel : EntityBase
    {
		private string _text;
		private decimal? _value;

		public string Text {
			get { return _text; }
			set {
				_text = value;
				OnPropertyChanged();
			}
		}

		public decimal? Value {
			get { return _value; }
			set {
				_value = value;
				OnPropertyChanged();
			}
		}
	}
}