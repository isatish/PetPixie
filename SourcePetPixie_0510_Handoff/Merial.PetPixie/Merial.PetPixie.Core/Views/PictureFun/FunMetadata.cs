using System;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{

    public class FunMetadata
    {
        public FunMetadata()
        {
            this.ItemWidth = 0;
            this.ItemHeight = 0;
            this.ItemRotation = 0;
            this.ItemX = 0;
            this.ItemY = 0;
            this.ItemImage = "";
            this.ItemScale = 1;
        }

        public float ItemWidth { get; set; }
        public float ItemScale { get; set; }
        public float ItemHeight { get; set; }
        public double ItemRotation { get; set; }
        public string ItemImage { get; set; }
        public double ItemX { get; set; }
        public double ItemY { get; set; }
    }

}
