using System;

namespace ctacke.Xamarin.CrossPlatformServices
{
    public interface INotificationService
    {
        void ShowNotification(string message, int duration);
        void ShowNotification(string message, string actionText, Action<object> action);
    }
}
