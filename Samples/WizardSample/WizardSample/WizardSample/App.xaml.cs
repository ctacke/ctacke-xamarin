using ctacke.Xamarin.MVVM;
using System;
using WizardSample.ViewModels;
using WizardSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WizardSample
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            NavigationService.Register<StartView, StartViewModel>();
            NavigationService.Register<MyWizardView, MyWizardViewModel>();
            NavigationService.Register<EndView, EndViewModel>();

            NavigationService.SetHomeView<StartView>(true);
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
