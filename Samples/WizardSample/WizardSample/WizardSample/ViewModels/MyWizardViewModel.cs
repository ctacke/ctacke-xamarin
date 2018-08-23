using ctacke.Xamarin.Forms;
using ctacke.Xamarin.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WizardSample.Views;
using WizardSample.Views.Steps;
using Xamarin.Forms;

namespace WizardSample.ViewModels
{
    class MyWizardViewModel : ViewModelBase
    {
        private string m_stepName;

        private List<WizardStepContent> m_steps = new List<WizardStepContent>();
        private WizardStepContent m_currentContent;
        private bool m_canNavigateForward;
        private bool m_canNavigateBackward;
        private string m_userName;

        public MyWizardViewModel()
        {
            var step = new WizardStep1();
            // You could use separate ViewModels per step.  
            // For simplicity of this sample, all steps will just use this ViewModel
            step.BindingContext = this;
            CurrentStepContent = step;
            CanNavigateForward = false;
        }

        public WizardStepContent CurrentStepContent
        {
            get => m_currentContent;
            set => SetProperty(ref m_currentContent, value);
        }

        public ICommand ForwardCommand
        {
            get =>
                new Command((toStep) =>
                {
                    WizardStepContent step = null;

                    if ((int)toStep >= m_steps.Count)
                    {
                        switch ((int)toStep)
                        {
                            case 1:
                                step = new WizardStep2();
                                step.BindingContext = this;
                                break;
                            case 2:
                                step = new WizardStep3();
                                step.BindingContext = this;
                                break;
                        }

                        if (step != null)
                        {
                            m_steps.Add(step);
                        }
                    }
                    else
                    {
                        step = m_steps[(int)toStep];
                    }

                    if (step != null)
                    {
                        CurrentStepContent = step;
                    }
                });
        }

        public ICommand BackwardCommand
        {
            get =>
                new Command((toStep) =>
                {
                    WizardStepContent step = null;

                    if ((int)toStep > 0)
                    {
                        step = m_steps[(int)toStep];
                        CurrentStepContent = step;
                    }
                });
        }

        public ICommand CancelledCommand
        {
            get =>
                new Command(async () =>
                {
                    await NavigationService.NavigateBack(false);
                });
        }

        public ICommand CompletedCommand
        {
            get =>
                new Command(async (toStep) =>
                {
                    await NavigationService.NavigateForward<EndView>();
                });
        }

        public string CurrentStepName
        {
            get => m_stepName;
            set => SetProperty(ref m_stepName, value);
        }

        public bool CanNavigateForward
        {
            get => m_canNavigateForward;
            set => SetProperty(ref m_canNavigateForward, value);
        }

        public bool CanNavigateBackward
        {
            get => m_canNavigateBackward;
            set => SetProperty(ref m_canNavigateBackward, value);
        }

        public string UserName
        {
            get => m_userName;
            set
            {
                SetProperty(ref m_userName, value);
                CanNavigateForward = !string.IsNullOrEmpty(UserName);
            }
        }
    }
}
