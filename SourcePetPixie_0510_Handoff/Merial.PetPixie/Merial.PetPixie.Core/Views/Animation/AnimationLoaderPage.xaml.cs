using System;
using System.Collections.Generic;
using SimpleTimerPortable;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class AnimationLoaderPage : ContentPage
    {
        private Timer timer;
        int currImage = 0;


        public AnimationLoaderPage()
        {
            InitializeComponent();
          //  HideAllFrames();
          

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        //    HideAllFrames();
            DoAnimation();
          //  Image0.IsVisible = true;
        }

            //private void CheckStatus(object sender, System.Timers.ElapsedEventArgs e)
            //{
            //    _countSeconds--;
            //    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            //    {
            //        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            //        {
            //            SpinAnimation();
            //        });
            //    })).Start();

            //    if (_countSeconds == 0)
            //    {
            //        _timer.Stop();
            //    }
            //}

        //public void HideAllFrames()
        //{
        //    Image0.IsVisible = false;
        //    Image1.IsVisible = false;
        //    Image2.IsVisible = false;
        //    Image3.IsVisible = false;
        //    Image4.IsVisible = false;
        //    Image5.IsVisible = false;
        //    Image6.IsVisible = false;
        //    Image7.IsVisible = false;
        //    Image8.IsVisible = false;
        //    Image9.IsVisible = false;
        //    Image10.IsVisible = false;
        //    Image11.IsVisible = false;
        //    Image12.IsVisible = false;
        //    Image13.IsVisible = false;
        //    Image14.IsVisible = false;
        //    Image15.IsVisible = false;
        //    Image16.IsVisible = false;
        //    Image17.IsVisible = false;
        //    Image18.IsVisible = false;
        //    Image19.IsVisible = false;
        //    Image20.IsVisible = false;
        //    Image21.IsVisible = false;
        //    Image22.IsVisible = false;
        //    Image23.IsVisible = false;
        //    Image24.IsVisible = false;
        //    Image25.IsVisible = false;
        //    Image26.IsVisible = false;
        //    Image27.IsVisible = false;
        //    Image28.IsVisible = false;
        //    Image29.IsVisible = false;
           

        //}

        private void DoAnimation()
        {
            if (timer == null)
            {
               // timer = new Timer(OnTick, null, 1000, 1000);
                 timer = new Timer(RunTick, null, 50, 50);
            }
        }

        private void RunTick(object args)
        {
            try
            {



                //_countSeconds--;


                //  new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                //  {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
             //       OnTick();
                });
                //  })).Start();

                //if (_countSeconds == 0)
                //{
                //    _timer.Stop();
                //}






            }
            catch (Exception exc)
            {
                string message = exc.Message;
            }

        }

        //private void OnTick( )
        //{
        //    try
        //    {

        //        //  HideAllFrames();
        //        switch (currImage)
        //        {
        //            case 0:
        //                Image29.IsVisible = false;
        //                Image0.IsVisible = true;
        //                break;

        //            case 1:
        //                Image0.IsVisible = false;
        //                Image1.IsVisible = true;
        //                break;

        //            case 2:
        //                Image1.IsVisible = false;
        //                Image2.IsVisible = true;
        //                break;


        //            case 3:
        //                Image2.IsVisible = false;
        //                Image3.IsVisible = true;
        //                break;


        //            case 4:
        //                Image3.IsVisible = false;
        //                Image4.IsVisible = true;
        //                break;


        //            case 5:
        //                Image4.IsVisible = false;
        //                Image5.IsVisible = true;
        //                break;

        //            case 6:
        //                Image5.IsVisible = false;
        //                Image6.IsVisible = true;
        //                break;

        //            case 7:
        //                Image6.IsVisible = false;
        //                Image7.IsVisible = true;
        //                break;


        //            case 8:
        //                Image7.IsVisible = false;
        //                Image8.IsVisible = true;
        //                break;


        //            case 9:
        //                Image8.IsVisible = false;
        //                Image9.IsVisible = true;
        //                break;


        //            case 10:
        //                Image9.IsVisible = false;
        //                Image10.IsVisible = true;
        //                break;

        //            case 11:
        //                Image10.IsVisible = false;
        //                Image11.IsVisible = true;
        //                break;

        //            case 12:
        //                Image11.IsVisible = false;
        //                Image12.IsVisible = true;
        //                break;


        //            case 13:
        //                Image12.IsVisible = false;
        //                Image13.IsVisible = true;
        //                break;


        //            case 14:
        //                Image13.IsVisible = false;
        //                Image14.IsVisible = true;
        //                break;


        //            case 15:
        //                Image14.IsVisible = false;
        //                Image15.IsVisible = true;
        //                break;

        //            case 16:
        //                Image15.IsVisible = false;
        //                Image16.IsVisible = true;
        //                break;

        //            case 17:
        //                Image16.IsVisible = false;
        //                Image17.IsVisible = true;
        //                break;


        //            case 18:
        //                Image17.IsVisible = false;
        //                Image18.IsVisible = true;
        //                break;


        //            case 19:
        //                Image18.IsVisible = false;
        //                Image19.IsVisible = true;
        //                break;


        //            case 20:
        //                Image19.IsVisible = false;
        //                Image20.IsVisible = true;
        //                break;


        //            case 21:
        //                Image20.IsVisible = false;
        //                Image21.IsVisible = true;
        //                break;

        //            case 22:
        //                Image21.IsVisible = false;
        //                Image22.IsVisible = true;
        //                break;


        //            case 23:
        //                Image22.IsVisible = false;
        //                Image23.IsVisible = true;
        //                break;


        //            case 24:
        //                Image23.IsVisible = false;
        //                Image24.IsVisible = true;
        //                break;


        //            case 25:
        //                Image24.IsVisible = false;
        //                Image25.IsVisible = true;
        //                break;

        //            case 26:
        //                Image25.IsVisible = false;
        //                Image26.IsVisible = true;
        //                break;

        //            case 27:
        //                Image26.IsVisible = false;
        //                Image27.IsVisible = true;
        //                break;


        //            case 28:
        //                Image27.IsVisible = false;
        //                Image28.IsVisible = true;
        //                break;


        //            case 29:
        //                Image28.IsVisible = false;
        //                Image29.IsVisible = true;
        //                currImage = -1;
        //                break;


        //        }
        //        currImage = currImage + 1;
        //    }

        //    catch (Exception exc)
        //    {
        //        string message = exc.Message;
        //    }
           

        //    //if (currImage == 0)
        //    //{
        //    //    _image0Visible = true;
        //    //}


        //    //    TimeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));

        //    //if (timeLeft.TotalSeconds == 0)
        //    //    ExecuteStopCommand();
        //}



    }
}
