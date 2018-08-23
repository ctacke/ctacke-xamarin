using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using ctacke.Xamarin.CrossPlatformServices;

namespace ctacke.Xamarin.Platform.Android
{
    public class AndroidNotificationService : INotificationService
    {
        private readonly Context m_appContext;

        public AndroidNotificationService(Context appContext)
        {
            m_appContext = appContext;
        }

        private View GetView()
        {
            var activity = m_appContext as Activity;
            if (activity == null)
            {
                // TODO: should we throw so a consumer knows we had a problem?
                return null;
            }
            var view = activity.FindViewById(global::Android.Resource.Id.Content);
            if (view == null)
            {
                // TODO: should we throw so a consumer knows we had a problem?
                return null;
            }

            return view;
        }

        public void ShowNotification(string message, int duration)
        {
            var view = GetView();
            if (view == null)
            {
                return;
            }

            var snack = Snackbar.Make(view, message, duration);
            snack.Show();
        }

        public void ShowNotification(string message, string actionText, Action<object> action)
        {
            var view = GetView();
            if (view == null)
            {
                return;
            }

            var snack = Snackbar.Make(view, message, Snackbar.LengthIndefinite)
                .SetAction(actionText, action);
            
            snack.Show();
        }
    }
}