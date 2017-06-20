using System;

namespace Merial.PetPixie.Core.Models {
	public class LatLngDistance {
		private const int EarthRadius = 6371;
		private readonly KLatLng _latLng;
		private readonly float _zoom;
		private readonly KLatLng _northeast;
		private readonly KLatLng _southwest;
		private double? _eastWestDistance;
		private double? _northSouthDistance;

		public LatLngDistance(KLatLng latLng, float zoom, KLatLng northeast, KLatLng southwest) {
			_latLng = latLng;
			_zoom = zoom;
			_northeast = northeast;
			_southwest = southwest;
		}

		public double Latitude => _latLng.Latitude;
		public double Longitude => _latLng.Longitude;

		public double EastWestDistance {
			get {
				if (!_eastWestDistance.HasValue) {
					_eastWestDistance = CalculateDistance(_northeast, new KLatLng(_northeast.Latitude, _southwest.Longitude));
				}
				return _eastWestDistance.Value;
			}
		}

		public double NorthSouthDistance {
			get {
				if (!_northSouthDistance.HasValue) {
					_northSouthDistance = CalculateDistance(_northeast, new KLatLng(_southwest.Latitude, _northeast.Longitude));
				}
				return _northSouthDistance.Value;
			}
		}

		private static double CalculateDistance(KLatLng origin, KLatLng destination) {
			var dLat = ToRadian(origin.Latitude - destination.Latitude); 
			var dLon = ToRadian(origin.Longitude - destination.Longitude);
			var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					Math.Cos(ToRadian(origin.Latitude)) * Math.Cos(ToRadian(destination.Latitude)) *
					Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return EarthRadius * c;
		}

		private static double ToRadian(double angle) {
			return Math.PI*angle/180.0;
		}
	}

	public class KLatLng {
		public KLatLng(double latitude, double longitude) {
			Latitude = latitude;
			Longitude = longitude;
		}

		public double Latitude { get; }
		public double Longitude { get; }
	}
}