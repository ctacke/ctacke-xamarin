using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctacke.Xamarin.Forms
{
    public sealed class ScaledGridLength : IMarkupExtension
    {
        private static ILayoutService s_layoutService;

        public string Length { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (s_layoutService == null)
            {
                s_layoutService = DependencyService.Get<ILayoutService>();
            }

            if (s_layoutService == null)
            {
                System.Diagnostics.Debug.WriteLine("ScaledGridLength has no ILayoutService!");
                
                var tc = new GridLengthTypeConverter();
                return tc.ConvertFromInvariantString(Length);
            }

            if (Length == "*")
            {
                return GridLength.Star;
            }
            else if (Length == "Auto")
            {
                return GridLength.Auto;
            }
            else
            {
                double d;
                if (double.TryParse(Length, out d))
                {
                    return new GridLength(s_layoutService.GetScaledDouble(d));
                }

                throw new Exception("Unable to convert to GridLength: " + Length);
            }
        }
    }
}
