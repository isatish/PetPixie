using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using Plugin.Connectivity;
//using Plugin.Connectivity.Abstractions;

namespace Merial.PetPixie.Core.ViewModels
{
    public class NoConnectivityViewModel : ViewModelBase
    {
        public override void Started()
        {
            //base.Started();
       //ds    ConnectivityService.AddConnectivityChangedListener(CurrentOnConnectivityChanged);

        }

        public override void Paused()
        {
            //base.Paused();
        //ds    ConnectivityService.RemoveConnectivityChangedListener(CurrentOnConnectivityChanged);
        }


        public IMvxCommand RetryConnectivity => new SafeMvxCommand(Execute);

        private void Execute()
        {
            if (ConnectivityService.IsConnected() == true)
            {
                Close(this);
            }
        }

        //private void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs)
        //{
        //    if (connectivityChangedEventArgs.IsConnected == true)
        //    {
        //        Close(this);
        //    }
        //}
    }
}
