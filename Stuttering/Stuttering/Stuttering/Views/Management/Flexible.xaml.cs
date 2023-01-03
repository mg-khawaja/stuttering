using Stuttering.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Flexible : ContentView
    {
        FlexibleViewModel vm;
        public Flexible()
        {
            try
            {
                vm = new FlexibleViewModel(Navigation);
                BindingContext = vm;
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            vm.GoToExerciseCommand.Execute((e as TappedEventArgs).Parameter);
        }
    }
}