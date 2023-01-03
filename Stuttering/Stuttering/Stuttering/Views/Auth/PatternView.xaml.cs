using Stuttering.Helper;
using Stuttering.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatternView : ContentPage
	{
		PatternViewModel vm;
		public PatternView ()
		{
			vm = new PatternViewModel(Navigation);
			BindingContext = vm;
			vm.IsBusy = true;
			InitializeComponent ();
			BindingContext = vm;
		}
		public PatternView(LockCheckType patternCheck)
		{
			vm = new PatternViewModel(Navigation);
			BindingContext = vm;
			vm.IsBusy = true;
			vm.checkType = patternCheck;
			InitializeComponent();
			BindingContext = vm;
		}
		protected override void OnAppearing()
        {
            base.OnAppearing();
			vm.OnAppearingCommand.Execute(null);
        }
        private async void pattern_GesturePatternCompleted(object sender, FaulandCc.XF.GesturePatternView.GesturePatternCompletedEventArgs e)
        {
			await vm.OnPatternCompleted(e.GesturePatternValue);
			pattern.Clear ();

        }
    }
}