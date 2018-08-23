using Android.Graphics;
using Android.Widget;
using ctacke.Xamarin.Forms.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(LabelEx), typeof(ctacke.Xamarin.Platform.Droid.LabelExRenderer))]
namespace ctacke.Xamarin.Platform.Droid
{
    public class LabelExRenderer : LabelRenderer
    {
        public LabelExRenderer(Context context)
            : base(context)
        {
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            
            var newControl = e.NewElement as LabelEx;

            Typeface typeface;

            if (newControl.FontSource.IsNullOrEmpty())
            {
                typeface = Typeface.Default;
            }
            else
            {
                typeface = Typeface.CreateFromAsset(Context.Assets, newControl.FontSource);
            }

            var ctrl = Control as TextView;
            if (ctrl != null)
            {
                ctrl.Typeface = typeface;
            }
        }
    }
}
