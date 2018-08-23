using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ctacke.Xamarin.Forms.Controls
{
    public class ButtonEx : Button
    {
        public static readonly BindableProperty FontSourceProperty =
            BindableProperty.Create("FontSource", typeof(string), typeof(LabelEx));

        public ButtonEx()
        {

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "FontSource")
            {
                SetValue(FontSourceProperty, FontSource);
            }
        }

        public string FontSource
        {
            get { return (string)GetValue(FontSourceProperty); }
            set { SetValue(FontSourceProperty, value); }
        }
    }
}
