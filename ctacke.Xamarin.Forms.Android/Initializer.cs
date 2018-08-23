using Android.App;
using Android.OS;

namespace ctacke.Xamarin.Forms
{
    public static class Initializer
    {
        internal static Activity AppActivity { get; private set; }
        internal static Bundle AppBundle { get; private set; }

        public static bool IsInitialized { get; private set; }

        public static void Init(Activity activity, Bundle bundle)
        {
            if (AppActivity != null)
            {
                throw new System.Exception("Init cannot be called multiple times.");
            }

            AppActivity = activity;
            AppBundle = bundle;

            IsInitialized = true;
        }
    }
}