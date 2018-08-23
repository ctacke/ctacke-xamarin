using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctacke.Xamarin.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WizardLayout : StackLayout
    {
        public static readonly BindableProperty CanNavigateForwardProperty =
            BindableProperty.Create("CanNavigateForward", typeof(bool), typeof(WizardLayout), defaultValue: true);

        public static readonly BindableProperty CanNavigateBackwardProperty =
            BindableProperty.Create("CanNavigateBackward", typeof(bool), typeof(WizardLayout), defaultValue: true);

        public static readonly BindableProperty IsForwardVisibleProperty =
            BindableProperty.Create("IsForwardVisible", typeof(bool), typeof(WizardLayout), defaultValue: true);

        public static readonly BindableProperty IsBackwardVisibleProperty =
            BindableProperty.Create("IsBackwardVisible", typeof(bool), typeof(WizardLayout), defaultValue: true);

        public static readonly BindableProperty ShowStepNameProperty =
            BindableProperty.Create("ShowStepName", typeof(bool), typeof(WizardLayout), defaultValue: true);

        public static readonly BindableProperty CurrentStepNameProperty =
            BindableProperty.Create("CurrentStepName", typeof(string), typeof(WizardLayout), defaultValue: null);

        public static readonly BindableProperty StepCountProperty =
            BindableProperty.Create("StepCount", typeof(int), typeof(WizardLayout), defaultValue: 1);

        public static readonly BindableProperty ForwardCommandProperty =
            BindableProperty.Create("ForwardCommand", typeof(ICommand), typeof(WizardLayout), defaultValue: null);

        public static readonly BindableProperty BackwardCommandProperty =
            BindableProperty.Create("BackwardCommand", typeof(ICommand), typeof(WizardLayout), defaultValue: null);

        public static readonly BindableProperty CurrentStepNumberProperty =
            BindableProperty.Create("CurrentStepNumber", typeof(int), typeof(WizardLayout), defaultValue: 0);

        public static readonly BindableProperty CurrentWizardStepContentProperty =
            BindableProperty.Create("CurrentWizardStepContent", typeof(WizardStepContent), typeof(WizardLayout), defaultValue: null);

        public static readonly BindableProperty WizardCompleteCommandProperty =
            BindableProperty.Create("WizardCompleteCommand", typeof(ICommand), typeof(WizardLayout), defaultValue: null);

        public static readonly BindableProperty WizardCancelledCommandProperty =
            BindableProperty.Create("WizardCancelledCommand", typeof(ICommand), typeof(WizardLayout), defaultValue: null);

        public WizardLayout()
		{
			InitializeComponent ();

            forwardButton.Clicked += OnForwardButtonClick;
            backButton.Clicked += OnBackButtonClicked;
		}

        private void WizardSteps_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (CurrentStepNumber > 0)
            {
                CurrentStepNumber--;
                if (BackwardCommand != null)
                {
                    if (BackwardCommand.CanExecute(CurrentStepNumber))
                    {
                        BackwardCommand.Execute(CurrentStepNumber);
                    }
                }
            }
            else
            {
                // user is backing out of the wizard
                if ((WizardCancelledCommand != null) && (WizardCancelledCommand.CanExecute(null)))
                {
                    WizardCancelledCommand.Execute(null);
                }
            }
        }

        private void OnForwardButtonClick(object sender, EventArgs e)
        {
            if (CurrentStepNumber < StepCount - 1)
            {
                CurrentStepNumber++;
                if ((ForwardCommand != null) && (ForwardCommand.CanExecute(CurrentStepNumber)))
                {
                    ForwardCommand.Execute(CurrentStepNumber);
                }
            }
            else
            {
                // finished last step
                if ((WizardCompleteCommand != null) && (WizardCompleteCommand.CanExecute(null)))
                {
                    WizardCompleteCommand.Execute(null);
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == CurrentStepNameProperty.PropertyName)
            {
                stepNameLabel.Text = CurrentStepName;
            }
            else if (propertyName == CurrentWizardStepContentProperty.PropertyName)
            {
                stepContentHolder.Content = CurrentWizardStepContent;
            }
            else if (propertyName == CanNavigateForwardProperty.PropertyName)
            {
                forwardButton.IsEnabled = CanNavigateForward;
            }
        }

        public bool CanNavigateForward
        {
            get { return (bool)GetValue(CanNavigateForwardProperty); }
            set { SetValue(CanNavigateForwardProperty, value); }
        }

        public bool CanNavigateBackward
        {
            get { return (bool)GetValue(CanNavigateBackwardProperty); }
            set { SetValue(CanNavigateBackwardProperty, value); }
        }

        public bool IsForwardVisible
        {
            get { return (bool)GetValue(IsForwardVisibleProperty); }
            set { SetValue(IsForwardVisibleProperty, value); }
        }

        public bool IsBackwardVisible
        {
            get { return (bool)GetValue(IsBackwardVisibleProperty); }
            set { SetValue(IsBackwardVisibleProperty, value); }
        }

        public bool ShowStepName
        {
            get { return (bool)GetValue(ShowStepNameProperty); }
            set { SetValue(ShowStepNameProperty, value); }
        }

        public string CurrentStepName
        {
            get { return (string)GetValue(CurrentStepNameProperty); }
            set { SetValue(CurrentStepNameProperty, value); }
        }

        public int CurrentStepNumber
        {
            get { return (int)GetValue(CurrentStepNumberProperty); }
            set { SetValue(CurrentStepNumberProperty, value); }
        }

        public int StepCount
        {
            get { return (int)GetValue(StepCountProperty); }
            set { SetValue(StepCountProperty, value); }
        }

        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        public ICommand BackwardCommand
        {
            get { return (ICommand)GetValue(BackwardCommandProperty); }
            set { SetValue(BackwardCommandProperty, value); }
        }

        public WizardStepContent CurrentWizardStepContent
        {
            get { return (WizardStepContent)GetValue(CurrentWizardStepContentProperty); }
            set { SetValue(CurrentWizardStepContentProperty, value); }
        }

        public ICommand WizardCompleteCommand
        {
            get { return (ICommand)GetValue(WizardCompleteCommandProperty); }
            set { SetValue(WizardCompleteCommandProperty, value); }
        }

        public ICommand WizardCancelledCommand
        {
            get { return (ICommand)GetValue(WizardCancelledCommandProperty); }
            set { SetValue(WizardCancelledCommandProperty, value); }
        }

        /*
         * Simply cannot get this to work.  Alwyas throws a NullRefeenceException at startup in InitializeComponent, but no further help.  Abandoning this for now, though it's definitely a better route
         * 
        public static readonly BindableProperty WizardStepsProperty = BindableProperty.Create(nameof(WizardSteps), typeof(ObservableCollection<WizardStepContent>), typeof(WizardLayout), defaultBindingMode: BindingMode.OneWay);

        public ObservableCollection<WizardStepContent> WizardSteps { get; } = new ObservableCollection<WizardStepContent>();
        */
    }
}