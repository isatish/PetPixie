using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
        public partial class FeedPageView : ContentView
        {
                public FeedPageView()
                {
                        InitializeComponent();
                        ShowInitialHeart();
                }



                void ShowInitialHeart()
                {

                        var dataContext = (FeedItemViewModel)this.BindingContext;
                        if (dataContext != null)
                        {
                                if (dataContext.YouLiked)
                                {
                                        animTwitter.Progress = 1;
                                }
                                else
                                {
                                 //       animTwitter.Progress = 1;
                                        animTwitter.Progress = 0;
                                }

                        }

                }


                void Handle_Tapped(object sender, System.EventArgs e)
                {

                        var dataContext = (FeedItemViewModel)this.BindingContext;
                        if (dataContext != null)
                        {
                                if (dataContext.YouLiked)
                                {
                                        animTwitter.Play();
                                }
                                else
                                {
                                        animTwitter.Progress = 1;
                                         animTwitter.Progress = 0;
                                        
                                        //animTwitter.Play();
                                   
                                }

                        }


                        //if (animTwitter.Progress == 1)
                        //{
                        //        animTwitter.Progress = 0;
                        //}
                        //else
                        //{
                        //        animTwitter.Play();
                        //}
                }

        }
}
