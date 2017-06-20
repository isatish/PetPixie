using Merial.PetPixie.Core.Models.EventHandler;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public delegate void LocationChangedEventHandler(object sender, LocationChangedEventArgs args);

    public interface ILocationService
    {
        bool? IsLocationEnabled { get; }

        void Start();

        void Stop();

        event LocationChangedEventHandler LocationChanged;

        LocationChangedEventArgs RequestLocation();
    }
}
