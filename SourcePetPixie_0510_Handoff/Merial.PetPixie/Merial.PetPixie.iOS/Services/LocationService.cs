using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLocation;
using Merial.PetPixie.Core.Models.EventHandler;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using LocationChangedEventArgs = Merial.PetPixie.Core.Models.EventHandler.LocationChangedEventArgs;

namespace Merial.PetPixie.iOS.Services
{
	public class LocationService : ILocationService
	{
		public bool? IsLocationEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public event LocationChangedEventHandler LocationChanged;

		public LocationChangedEventArgs RequestLocation()
		{
			//throw new NotImplementedException();


            LocationManager locationManager = new LocationManager();

            return new LocationChangedEventArgs();
         //   locationManager.RequestWhenInUseAuthorization();
          //  mapView.ShowsUserLocation = true;


		}

		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}
	}
}