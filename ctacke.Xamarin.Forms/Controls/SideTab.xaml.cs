using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ctacke.Xamarin.Forms.Controls
{
    public partial class SideTab : RelativeLayout
    {
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(bool), typeof(SideTab), defaultBindingMode: BindingMode.TwoWay, defaultValue: false);

        public static readonly BindableProperty ContentProperty =
            BindableProperty.CreateAttached("Content", typeof(View), typeof(SideTab), null);

        public int TabIndex { get; internal set; }

        public Image tabImage;
        public Image selectedImage;
        public ContentView contentContainer;

        public SideTab()
        {
            InitializeComponent();

            tabImage = this.Children[0] as Image;
            selectedImage = this.Children[1] as Image;
            contentContainer = this.Children[2] as ContentView;

            this.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OnTapped())
            });
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == IsSelectedProperty.PropertyName)
            {
                IsSelected = (bool)GetValue(IsSelectedProperty);
            }
        }

        private void OnTapped()
        {
            var parent = this.Parent as SideTabsLayout;
            if (parent != null)
            {
                parent.SelectedTab = this;
                parent.RaiseCommand(this);
            }
        }

        public bool IsSelected
        {
            get
            {
                var parent = this.Parent as SideTabsLayout;
                if (parent != null)
                {
                    return this == parent.SelectedTab;
                }
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
                var parent = this.Parent as SideTabsLayout;
                if (parent != null)
                {
                    if (IsSelected)
                    {
                        parent.SelectedTab = this;
                    }
                }
            }
        }

        internal void ChangeSelectionImage(bool selected)
        {
            if (SelectedTabImage == null) return;

            if (selected)
            {
                selectedImage.FadeTo(1.0, 100);
                tabImage.FadeTo(0.0, 100);
            }
            else
            {
                selectedImage.FadeTo(0.0, 100);
                tabImage.FadeTo(1.0, 100);
            }
        }

        internal ImageSource TabImage
        {
            get { return tabImage.Source; }
            set { tabImage.Source = value; }
        }

        internal ImageSource SelectedTabImage
        {
            get { return selectedImage.Source; }
            set { selectedImage.Source = value; }
        }

        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        internal void SetContent(View content)
        {
            contentContainer.Content = content;
        }
    }
}