using System;
using ctacke.Xamarin.CrossPlatformServices;
using ctacke.Xamarin.Forms.Android.Services;
using ctacke.Xamarin.Platform.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidServiceFactory))]

namespace ctacke.Xamarin.Forms.Android.Services
{
    class AndroidServiceFactory : IServiceFactory
    {
        public INotificationService GetNotificationService()
        {
            var existing = DependencyService.Get<INotificationService>();
            if (existing == null)
            {
                if (Initializer.AppActivity == null)
                {
                    throw new Exception("ctacke.Xamarin.Forms.Initializer.Init() has not been called.");
                }

                existing = new AndroidNotificationService(Initializer.AppActivity);
            }

            return existing;
        }

        public IPlatformSettingsService GetPlatformSettingsService()
        {
            var existing = DependencyService.Get<IPlatformSettingsService>();
            if (existing == null)
            {
                if (Initializer.AppActivity == null)
                {
                    throw new Exception("ctacke.Xamarin.Forms.Initializer.Init() has not been called.");
                }

                existing = new AndroidSettingsService(Initializer.AppActivity);
            }

            return existing;
        }
    }
}