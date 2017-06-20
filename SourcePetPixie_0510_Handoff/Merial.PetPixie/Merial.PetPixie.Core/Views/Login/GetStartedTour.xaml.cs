using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
        public partial class GetStartedTour : ContentView
        {


                public GetStartedTour()
                {
                        InitializeComponent();
                        this.MyCarouselView.ItemsSource = new List<int> { 1, 2, 3, 4, 5 };
                        this.MyCarouselView.Position = 0;
                        this.MyCarouselView.BackgroundColor = Color.Transparent;
                        //	SetNavigationBarProperties();
                //        MyCarouselView.WidthRequest = this.Width;
                  //      MyCarouselView.HeightRequest = this.Height;

                }

                void Handle_CloseClicked(object sender, System.EventArgs e)
                {
                        this.IsVisible = false;
                        MessagingCenter.Send(this, "HideTour");
                }
        }
}
