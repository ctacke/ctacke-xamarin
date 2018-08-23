using Xamarin.Forms;

[assembly: Dependency(typeof(ctacke.Xamarin.Platform.Android.AndroidLayoutService))]

namespace ctacke.Xamarin.Platform.Android
{
    public sealed class AndroidLayoutService : ILayoutService
    {
        private static double s_standardWidthInches = 3.75;
        private static double s_widthScaleFactor = (double)global::Android.App.Application.Context.Resources.DisplayMetrics.WidthPixels / (double)global::Android.App.Application.Context.Resources.DisplayMetrics.DensityDpi / s_standardWidthInches;

        public AndroidLayoutService()
        {
        }

        public double GetScaledDouble(double fontSize)
        {
            return fontSize * s_widthScaleFactor;
        }

        public int GetScaledInt32(int original)
        {
            return System.Convert.ToInt32(GetScaledDouble(original));
        }
    }
}
