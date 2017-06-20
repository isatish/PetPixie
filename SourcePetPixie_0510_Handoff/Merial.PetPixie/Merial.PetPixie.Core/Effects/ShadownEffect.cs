using System;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Effects
{
    public class ShadownEffect
    {
        public class ShadowEffect : RoutingEffect
        {
            public float Radius { get; set; }

            public Color Color { get; set; }

            public float DistanceX { get; set; }

            public float DistanceY { get; set; }

            public ShadowEffect() : base("Merial.PetPixie.Core.Effects.ShadowEffect")
            {
            }
        }
    }

}
