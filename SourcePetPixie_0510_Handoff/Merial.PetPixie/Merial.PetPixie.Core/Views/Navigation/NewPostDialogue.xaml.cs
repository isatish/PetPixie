using System;
using System.Collections.Generic;
using Plugin.Media;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class NewPostDialog : ContentView
    {

        public NewPostDialog()
        {
            InitializeComponent();
        }



        void Handle_CloseClicked(object sender, System.EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}
