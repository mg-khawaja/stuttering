using Stuttering.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        ExerciseViewModel vm;
        public Exercise(Models.Exercise exercise)
        {
            try
            {
                
                vm = new ExerciseViewModel(Navigation, exercise);
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
        protected override void OnDisappearing()
        {
            //base.OnDisappearing();
            //try
            //{
            //    vm.player.Stop(true);
            //}
            //catch (Exception)
            //{

            //}
        }
        protected override bool OnBackButtonPressed()
        {
            try
            {
                vm.player.Stop();
            }
            catch (Exception)
            {

            }
            return base.OnBackButtonPressed();
        }
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                vm.player.Stop();
            }
            catch (Exception)
            {

            }
            Navigation.PopAsync();
        }
    }
}