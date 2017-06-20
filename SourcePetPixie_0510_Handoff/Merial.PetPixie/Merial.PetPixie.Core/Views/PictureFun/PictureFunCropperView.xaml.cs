using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class PictureFunCropperView : MR.Gestures.ContentView
    {


        public PictureFunCropperView()
        {
            InitializeComponent();
            BindingContext = new TransformImageViewModel();


        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

