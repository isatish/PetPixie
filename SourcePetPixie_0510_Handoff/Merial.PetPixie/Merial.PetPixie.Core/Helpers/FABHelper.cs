using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAB.Forms;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.Views;
using Plugin.Media;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Helpers
{
    public static class FABHelper
    {





        public static void SetFABProperties(TopNavBar navBar, View listItems, ContentPage contentContainer, string loaderMessage = "")
        {

            var dialogPost = new NewPostDialog() { IsVisible = false };

            var fab = new FloatingActionButton()
            {
                Source = "LogoSquareTiny.png",
                NormalColor = Color.FromHex("#e23229"),
                RippleColor = Color.FromHex("#e23229"),
            };

            //var loader = new StackLayout() { WidthRequest = 300, HeightRequest = 400, BackgroundColor = Color.Yellow };
            //NOTE: This logic should be seperated out on  
            var loader = new Shared.Loader();
            loader.SetLoadingMessage(loaderMessage);
            


            fab.Clicked += (sender, e) =>
            {
                dialogPost.IsVisible = !dialogPost.IsVisible;
              //  MessagingCenter.Send(this, "FabClicked");
            };



            // Main page layout
            var pageLayout = new StackLayout
            {
                Children =
                {
                    navBar,
                    listItems
                }
            };


            var dialogLayout = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(dialogLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(dialogLayout, new Rectangle(0f, 0f, 1f, 1f));
            dialogLayout.Children.Add(dialogPost);

            // Overlay the FAB in the bottom-right corner
            AbsoluteLayout.SetLayoutFlags(dialogPost, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(dialogPost, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            dialogPost.Margin = new Thickness(20);


            var absolute = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(pageLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(pageLayout, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(pageLayout);

            // Overlay the FAB in the bottom-right corner
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            fab.Margin = new Thickness(20);
            fab.Size = FabSize.Normal;
            absolute.Children.Add(dialogPost);
            absolute.Children.Add(fab);
            // absolute.Children.Add(leftNav);


            //Add Loader

            AbsoluteLayout.SetLayoutFlags(loader, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loader, new Rectangle(0.5f, 0.5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(loader);

            contentContainer.Content = absolute;


        //    var loader = new AnimationLoaderPage();



        }




    }
}
