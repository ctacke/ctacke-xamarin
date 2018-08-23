using ctacke.Xamarin.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WizardSample.Views;
using Xamarin.Forms;

namespace WizardSample.ViewModels
{
    class StartViewModel : ViewModelBase
    {
        public ICommand StartWizardCommand
        {
            get =>
                new Command(async () =>
                {
                    await NavigationService.NavigateForward<MyWizardView>();
                });
        }
    }
}
