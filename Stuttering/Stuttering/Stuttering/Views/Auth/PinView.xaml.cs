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
	public partial class PinView : ContentPage
	{
		PinViewModel vm;
		public PinView()
		{
			vm = new PinViewModel(Navigation);
			vm.IsBusy = true;
			BindingContext = vm;
			vm.IsBusy = true;
			InitializeComponent ();
			BindingContext = vm;
		}

        public PinView(LockCheckType patternCheck)
		{
			vm = new PinViewModel(Navigation);
			vm.IsBusy = true;
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

        private async void GoBtn_Clicked(object sender, EventArgs e)
        {
			await vm.OnPinCompleted(otp.SelectedOtp);
			((otp as Grid).Children[0] as Entry).Text = "";
			//otp.EntryList[0].Text = "";
			//otp.EntryList[1].Text = "";
			//otp.EntryList[2].Text = "";
			//otp.EntryList[3].Text = "";
        }
    }
}