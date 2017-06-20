using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public partial class PictureFunModView :  MR.Gestures.ContentView
    {

        private string _funFileName = "";

        public PictureFunModView()
        {
            InitializeComponent();
            BindingContext = new TransformImageViewModel();

        }

        public PictureFunModView(string defaultFileName)
        {
            InitializeComponent();
            BindingContext = new TransformImageViewModel();

            if (defaultFileName != null)
            {
                _funFileName = defaultFileName;
                ImageItem.Source = defaultFileName;
             //   ImageItem.TranslationY = 0;
             //   ImageItem.TranslationX = 0;
            }
 
        }

        public string FunFileName
        {
            get { return _funFileName; }

        }

        public Image FunImageContent()
        {

            return ImageItem;
        }
      
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

