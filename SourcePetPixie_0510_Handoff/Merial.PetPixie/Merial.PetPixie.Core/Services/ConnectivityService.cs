using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Services.Contracts;
using Plugin.Connectivity;

namespace Merial.PetPixie.Core.Services
{
    public class ConnectivityService :IConnectivityService
    {
      
        public bool IsConnected()
        {
            return true;
           //ds return CrossConnectivity.Current.IsConnected;
        }

        //public void AddConnectivityChangedListener(ConnectivityChangedEventHandler currentOnConnectivityChanged)
        //{
        ////ds    CrossConnectivity.Current.ConnectivityChanged += currentOnConnectivityChanged;
        //}

        //public void RemoveConnectivityChangedListener(ConnectivityChangedEventHandler currentOnConnectivityChanged)
        //{
        //  //ds  CrossConnectivity.Current.ConnectivityChanged-= currentOnConnectivityChanged;
        //}

     
    }
}
