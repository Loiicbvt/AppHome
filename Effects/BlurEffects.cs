using Microsoft.Maui.Controls;

namespace HomeAppLBO.Effects
{
    public sealed class BlurEffect : RoutingEffect
    {
        public int Radius { get; set; }

        public BlurEffect() : base("HomeAppLBO.BlurEffect")
        {
            Radius = 15;
        }
    }
}