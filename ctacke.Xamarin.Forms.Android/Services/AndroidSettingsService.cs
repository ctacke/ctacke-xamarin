using Android.Content;
using ctacke.Xamarin.CrossPlatformServices;

namespace ctacke.Xamarin.Platform.Android
{
    public class AndroidSettingsService : IPlatformSettingsService
    {
        private readonly Context m_appContext;

        public AndroidSettingsService(Context appContext)
        {
            m_appContext = appContext;
        }

        public void ShowLocationSettings()
        {
            m_appContext.StartActivity(new global::Android.Content.Intent(global::Android.Provider.Settings.ActionLocat‌​ionSourceSettings));
        }
    }
}