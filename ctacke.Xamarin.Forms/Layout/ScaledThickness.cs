using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctacke.Xamarin.Forms
{
    public sealed class ScaledThickness : IMarkupExtension
    {
        private static ILayoutService s_layoutService;

        public string Thickness { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (s_layoutService == null)
            {
                s_layoutService = DependencyService.Get<ILayoutService>();
            }

            if (s_layoutService == null)
            {
                System.Diagnostics.Debug.WriteLine("ScaledThickness has no ILayoutService!");

                var tc = new ThicknessTypeConverter();
                return tc.ConvertFromInvariantString(Thickness);
            }

            var sizeStrings = Thickness.Split(',');
            var sizes = new int[sizeStrings.Length];
            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = s_layoutService.GetScaledInt32(Convert.ToInt32(sizeStrings[i]));
            }

            switch (sizes.Length)
            {
                case 1:
                    return new Thickness(sizes[0]);
                case 2:
                    return new Thickness(sizes[0], sizes[1]);
                case 4:
                    return new Thickness(sizes[0], sizes[1], sizes[2], sizes[3]);
            }

            throw new Exception("Unable to convert to ScaledThickness: " + Thickness);
        }
    }
}
