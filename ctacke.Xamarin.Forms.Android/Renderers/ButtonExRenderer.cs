using Android.Graphics;
using ctacke.Xamarin.Forms.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWB = Android.Widget.Button; // due to 'Android' namespace collision
using Android.Content;

[assembly: ExportRenderer(typeof(ButtonEx), typeof(ctacke.Xamarin.Platform.Droid.ButtonExRenderer))]
namespace ctacke.Xamarin.Platform.Droid
{
    public class ButtonExRenderer : ButtonRenderer
    {
        public ButtonExRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var newControl = e.NewElement as ButtonEx;

            Typeface typeface;

            if (newControl.FontSource.IsNullOrEmpty())
            {
                typeface = Typeface.Default;
            }
            else
            {
                typeface = Typeface.CreateFromAsset(Context.Assets, newControl.FontSource);
            }

            var ctrl = Control as AWB;
            if (ctrl != null)
            {
                ctrl.Typeface = typeface;
            }
        }
    }
}
