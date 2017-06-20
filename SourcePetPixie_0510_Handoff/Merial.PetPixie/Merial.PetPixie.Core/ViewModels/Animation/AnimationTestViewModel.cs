//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Merial.PetPixie.Core.Helpers;
////dslocalization using Merial.PetPixie.Core.Resources;
//using Merial.PetPixie.Core.Services.Contracts;
//using Merial.PetPixie.Core.ViewModels.Core;
//using MvvmCross.Core.ViewModels;
//using MvvmCross.Platform;

//namespace Merial.PetPixie.Core.ViewModels
//{
//    public class AnimationTestViewModel : ViewModelBase
//    {
//        e

//  //        public AnimationTestViewModel(IFeedService feedService, IUserService userService, IBitmapService bitmapService)
//  //        {
//  //            _feedService = feedService;
//  //            _userService = userService;
//  //            _bitmapService = bitmapService;
//  //            _items = new ObservableCollection<FeedItemViewModel>();/
//  //        }

//  //}
//  //}





//  //using Merial.PetPixie.Core.Models;
//  //using Merial.PetPixie.Core.Services.Contracts;
//  //using Merial.PetPixie.Core.ViewModels.Core;
//  //using System.Collections.ObjectModel;
//  //using System.Linq;
//  //using System.Windows.Input;
//  //using Merial.PetPixie.Core.Helpers;
//  //using MvvmCross.Core.ViewModels;
//  //using System.Collections.Generic;
//  //using Merial.PetPixie.Core.Models.Enums;
//  //using Merial.PetPixie.Core.ViewModels;
//  //using Merial.PetPixie.Core.ViewModels.Reminder;
//  //using MvvmCross.Plugins.PictureChooser;
//  //using MvvmCross.Platform;
//  //using System.IO;
//  //using KinveyXamarin;
//  //using Acr.UserDialogs;
//  //using System;
//  //using Plugin.Media;
//  //using Splat;
//  //using Xamarin.Forms;
//  //using SimpleTimerPortable;
//  ////using Merial.PetPixie.Core.ViewModels.Merial.PetPixie.Core.ViewModels;

//  //namespace Merial.PetPixie.Core.ViewModels
//  //{
//  //    public class AnimationTestViewModel : ViewModelBase
//  //    {
//  //        //#region Fieds


//  //        private Timer timer;
//  //        //private int _countSeconds = 0;






//  //        //#endregion




//  ////        #region

//  ////        private ICommand stopCommand;
//  ////        public ICommand StopCommand
//  ////        {
//  ////            get { return stopCommand ?? (stopCommand = new RelayCommand(ExecuteStopCommand)); }
//  ////        }

//  ////        private void ExecuteStopCommand()
//  ////        {

//  ////            TimeLeft = new TimeSpan(0, 4, 0);

//  ////            if (timer == null)
//  ////                return;

//  ////            timer.Dispose();
//  ////            timer = null;
//  ////            TimeLeft = new TimeSpan(0, 4, 0);
//  ////        }

//  ////#endregion







//  //        #region Constructors
//                                 ge



////        
////        //public AnimationTestViewModel(){
////        // //    if(timer == null)
////        ////        timer = new Timer(OnTick, null, 1000, 1000);
////        //}


//        //
//        //        //private void OnTick(object args)
//        //        //{
//        //        //    TimeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));

//        //        //    if (timeLeft.TotalSeconds == 0)
//        //        //        ExecuteStopCommand();
//        //        //}

//        //    //private TimeSpan timeLeft = new TimeSpan(0, 4, 0);
//        //    //public TimeSpan TimeLeft
//        //    //{
//        //    //  get { return timeLeft; }
//        //    //  set
//        //    //        {
//        //    //            timeLeft = value;
//        //    //            //OnPropertyChanged("TimeLeft");
//        //    //            RaisePropertyChanged(() => TimeLeft);
//        //    //            //Time = string.Format("{0:m\\:ss}", timeLeft);
//        //    //        }
//        //    //}


//        //        private void DoAnimation()
//        //        {
//        //              if(timer == null)
//        //                timer = new Timer(OnTick, null, 1000, 1000); 



//        //            //_timer = new System.Timers;

//        //            //Trigger event every second
//        //            //_timer.Interval = 1000;
//        //            //_timer.Elapsed += CheckStatus;
//        //            //count down 5 seconds
//        //            //_countSeconds = 5000;
//        //            //_timer.Enabled = true;

//        //        }
//        //        //private void SpinAnimation()
//        //        //{
//        //        //    switch (_letterShowState)
//        //        //    {
//        //        //        case SomeStateStates.State1:
//        //        //            _pic1.IsVisible = false;
//        //        //            _pic2.IsVisible = true;
//        //        //            _pic3.IsVisible = false;
//        //        //            _letterShowState = SomeStateStates.State2;
//        //        //            break;
//        //        //    }
//        //        //}

//        //        //private void CheckStatus(object sender, System.Timers.ElapsedEventArgs e)
//        //        //{
//        //        //    _countSeconds--;
//        //        //    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
//        //        //    {
//        //        //        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
//        //        //        {
//        //        //            SpinAnimation();
//        //        //        });
//        //        //    })).Start();

//        //        //    if (_countSeconds == 0)
//        //        //    {
//        //        //        _timer.Stop();
//        //        //    }
//        //        //}
//        //        #endregion






//        //    }
//        //}



//
//    }