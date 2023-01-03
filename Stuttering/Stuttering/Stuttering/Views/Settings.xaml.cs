using Stuttering.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentView
    {
        SettingsViewModel vm;
        public Settings()
        {
            vm = new SettingsViewModel(Navigation);
            BindingContext = vm;
            vm.OnAppearingCommand.Execute(this);
            InitializeComponent();
        }
        private void PatternSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            vm.SwitchCommand.Execute(e.Value);
        }
    }
}