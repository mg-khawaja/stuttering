using Stuttering.ViewModels;
using Xamarin.Forms;

namespace Stuttering.Views
{
    public partial class StutteringOverviewPage : ContentView
    {
        //StutteringOverviewViewModel _viewModel;

        public StutteringOverviewPage()
        {
            InitializeComponent();
            //BindingContext = _viewModel = new StutteringOverviewViewModel();
            triggers.AnimationEasing = Easing.SpringIn;
            triggers.AnimationLength = 10;
            emotional.AnimationEasing = Easing.SpringIn;
            emotional.AnimationLength = 10;
            varies.AnimationEasing = Easing.SpringIn;
            varies.AnimationLength = 10;
            nothing.AnimationEasing = Easing.SpringIn;
            nothing.AnimationLength = 10;
            management.AnimationEasing = Easing.SpringIn;
            management.AnimationLength = 10;
            facts.AnimationEasing = Easing.SpringIn;
            facts.AnimationLength = 10;
            motivational.AnimationEasing = Easing.SpringIn;
            motivational.AnimationLength = 10;
            journey.AnimationEasing = Easing.SpringIn;
            journey.AnimationLength = 10;
            hierarchy.AnimationEasing = Easing.SpringIn;
            hierarchy.AnimationLength = 10;
            if (Helper.GlobalSettings.CurrentUser.Language == "ur")
            {
                journeyImage.Source = "journeyUrdu.jpeg";
                hierarchyImage.Source = "hierarchyUrdu.jpeg";
            }
        }
    }
}