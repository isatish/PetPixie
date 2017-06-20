using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Merial.PetPixie.Core.Views
{
	public class CustomMap : Map
	{
		public List<CustomPin> CustomPins { get; set; }
	}
}
