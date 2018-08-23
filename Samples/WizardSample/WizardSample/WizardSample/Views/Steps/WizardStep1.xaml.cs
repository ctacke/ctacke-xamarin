using ctacke.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizardSample.Views.Steps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WizardStep1 : WizardStepContent
    {
		public WizardStep1 ()
		{
			InitializeComponent ();
		}
	}
}