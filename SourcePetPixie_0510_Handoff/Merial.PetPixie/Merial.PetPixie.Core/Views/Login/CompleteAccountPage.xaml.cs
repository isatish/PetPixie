using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.ViewModels;
using System;

namespace Merial.PetPixie.Core.Views
{
        public partial class CompleteAccountPage : ContentPage
        {





                public CompleteAccountPage()
                {
                        InitializeComponent();
                        SetNavigationBarProperties();

                }

                void Handle_CreateClicked(object sender, System.EventArgs e)
                {

                        var loader = new Shared.Loader();

                        loader.SetLoadingMessage("Seting up your account");
                        stackLoader.Children.Clear();
                        stackLoader.Children.Add(loader);
                }

                public void SetNavigationBarProperties()
                {

                        NavigationPage.SetHasNavigationBar(this, false);
                        //      navBar.ShowHeaderText("Complete Account");

                }



        }
}