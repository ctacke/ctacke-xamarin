using ctacke.Xamarin.CrossPlatformServices;

namespace ctacke.Xamarin
{
    public interface IServiceFactory
    {
        INotificationService GetNotificationService();
        IPlatformSettingsService GetPlatformSettingsService();
    }

}
