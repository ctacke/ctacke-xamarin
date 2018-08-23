using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctacke.Xamarin.Forms
{
    public sealed class ScaledDouble : IMarkupExtension
    {
        private static ILayoutService s_layoutService;

        public double Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (s_layoutService == null)
            {
                s_layoutService = DependencyService.Get<ILayoutService>();
            }

            if (s_layoutService == null)
            {
                System.Diagnostics.Debug.WriteLine("ScaledDouble has no ILayoutService!");

                return Value;
            }

            var scaled = s_layoutService.GetScaledDouble(Value);

            return scaled;
        }
    }
}
