using System.Windows.Input;
using Xamarin.Forms;

namespace ctacke.Xamarin.Forms.Controls
{
    public class SideTabsLayout : Layout<SideTab>
    {
        public static readonly BindableProperty TabImageProperty =
            BindableProperty.Create("TabImage", typeof(ImageSource), typeof(SideTabsLayout));

        public static readonly BindableProperty SelectedTabImageProperty =
            BindableProperty.Create("SelectedTabImage", typeof(ImageSource), typeof(SideTabsLayout));

        public static readonly BindableProperty TabSpacingProperty =
            BindableProperty.Create("TabSpacing", typeof(int), typeof(SideTabsLayout), -5);

        public static readonly BindableProperty TabHeightProperty =
            BindableProperty.Create("TabHeight", typeof(int), typeof(SideTabsLayout), 100);

        public static readonly BindableProperty TabWidthProperty =
            BindableProperty.Create("TabWidth", typeof(int), typeof(SideTabsLayout), 50);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SideTabsLayout), null);

        private SideTab m_selectedTab;

        public SideTabsLayout()
        {
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ImageSource TabImage
        {
            get { return (ImageSource)GetValue(TabImageProperty); }
            set { SetValue(TabImageProperty, value); }
        }

        public int TabHeight
        {
            get { return (int)GetValue(TabHeightProperty); }
            set { SetValue(TabImageProperty, value); }
        }

        public int TabSpacing
        {
            get { return (int)GetValue(TabSpacingProperty); }
            set { SetValue(TabSpacingProperty, value); }
        }

        public int TabWidth
        {
            get { return (int)GetValue(TabWidthProperty); }
            set { SetValue(TabImageProperty, value); }
        }

        public ImageSource SelectedTabImage
        {
            get
            {
                var img = (ImageSource)GetValue(SelectedTabImageProperty);
                if (img == null)
                {
                    // if no selected image is specified, just return the default image
                    return TabImage;
                }

                return img;
            }
            set { SetValue(SelectedTabImageProperty, value); }
        }

        public int TabIndex
        {
            get
            {
                if (m_selectedTab == null) return -1;
                return m_selectedTab.TabIndex;
            }
            set
            {
                foreach (SideTab tab in Children)
                {
                    if (tab.TabIndex == value)
                    {
                        SelectedTab = tab;
                        return;
                    }
                }
            }
        }

        public SideTab SelectedTab
        {
            get { return m_selectedTab; } 
            set
            {
                if (value == SelectedTab) return;

                if (SelectedTab != null)
                {
                    // unselect the old one
                    SelectedTab.IsSelected = false;
                    SetTabImage(SelectedTab, false);
                }

                m_selectedTab = value;

                value.IsSelected = true;
                SetTabImage(value, true);
            }
        }

        internal void RaiseCommand(SideTab tab)
        {
            if (Command != null)
            {
                Command.Execute(tab);
            }
        }

        private void SetTabImage(SideTab tab, bool isSelected)
        {
            if (tab != null)
            {
                tab.ChangeSelectionImage(isSelected);
            }
        }

        protected override void OnChildAdded(Element child)
        {
            var tab = child as SideTab;

            if (tab != null)
            {
                tab.TabImage = this.TabImage;
                tab.SelectedTabImage = this.SelectedTabImage;

                if (tab.IsSelected)
                {
                    SelectedTab = tab;
                }

                if (tab.Content != null)
                {
                    tab.SetContent(tab.Content);
                }

                // TODO: this is only good on Add.  If we start Inserting, it's going to fall apart
                tab.TabIndex = this.Children.Count - 1;
            }

            base.OnChildAdded(child);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var xPos = x;
            var yPos = y;

            foreach (var child in Children)
            {
                var region = new Rectangle(xPos, yPos, TabWidth, TabHeight);
                LayoutChildIntoBoundingRegion(
                    child,
                    region);

                yPos += (int)((double)TabHeight + TabSpacing);
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var width = TabWidth;
            var height = 0;

            foreach (var child in Children)
            {
                height += (int)((double)TabHeight + TabSpacing);
            }

            return new SizeRequest(new Size(width, height));
        }
    }
}
