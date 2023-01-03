using Stuttering.Models;
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
    public partial class ExercisesList : ContentPage
    {
        ExercisesListViewModel vm;
        public ExercisesList(Chapter chapter)
        {
            try
            {
                vm = new ExercisesListViewModel(Navigation, chapter);
                BindingContext = vm;
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearingCommand.Execute(null);
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            vm.GoToExerciseCommand.Execute((e as TappedEventArgs).Parameter);
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}