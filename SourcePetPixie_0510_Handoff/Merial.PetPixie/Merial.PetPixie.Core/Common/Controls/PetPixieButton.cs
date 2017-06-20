using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace Merial.PetPixie.Core.Shared
{
    public class PetPixieButton : Button
    {
        public PetPixieButton() : base()
        {
            const int _animationTime = 100;
            Clicked += async (sender, e) =>
            {
                var btn = (Button)sender;
                await btn.ScaleTo(1.2, _animationTime);
                btn.ScaleTo(1, _animationTime);
            };
        }
    }
}