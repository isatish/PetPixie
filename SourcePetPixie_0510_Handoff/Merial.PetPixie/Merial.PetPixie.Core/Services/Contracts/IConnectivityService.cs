using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IConnectivityService
    {

        bool IsConnected();
     //ds   void AddConnectivityChangedListener(ConnectivityChangedEventHandler currentOnConnectivityChanged);
     //ds   void RemoveConnectivityChangedListener(ConnectivityChangedEventHandler currentOnConnectivityChanged);
    }
}
