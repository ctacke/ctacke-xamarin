using System;
using Xamarin.Forms;

namespace ctacke.Xamarin.CrossPlatformServices
{
    public static class Resolver
    {
        private static IServiceFactory m_serviceFactory;

        private static IServiceFactory ServiceFactory
        {
            get
            {
                if (m_serviceFactory == null)
                {
                    m_serviceFactory = DependencyService.Get<IServiceFactory>();

                    if (m_serviceFactory == null)
                    {
                        throw new Exception("ctacke.Xamarin.Forms.Initializer.Init() has not been called.");
                    }
                }

                return m_serviceFactory;
            }
        }

        public static INotificationService GetNotificationService()
        {
            return ServiceFactory.GetNotificationService();
        }

        public static IPlatformSettingsService GetPlatformSettingsService()
        {
            return ServiceFactory.GetPlatformSettingsService();
        }

    }
}
