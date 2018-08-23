using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctacke.Xamarin.Forms
{
    public class ScaledInteger : IMarkupExtension
    {
        private static ILayoutService s_layoutService;

        public int Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (s_layoutService == null)
            {
                s_layoutService = DependencyService.Get<ILayoutService>();
            }

            if (s_layoutService == null)
            {
                System.Diagnostics.Debug.WriteLine("ScaledInteger has no ILayoutService!");

                return Value;
            }
            var scaled = s_layoutService.GetScaledInt32(Value);

            return (int)scaled;
        }
    }
}