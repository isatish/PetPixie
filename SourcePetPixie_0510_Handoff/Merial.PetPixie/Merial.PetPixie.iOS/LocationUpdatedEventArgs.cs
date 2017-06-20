using System;
using CoreLocation;

namespace Merial.PetPixie.iOS
{
    public class LocationUpdatedEventArgs : EventArgs
    {
        CLLocation location;

        public LocationUpdatedEventArgs(CLLocation location)
        {
            this.location = location;
        }

        public CLLocation Location
        {
            get { return location; }
        }
    }
}
