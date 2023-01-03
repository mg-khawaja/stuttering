using Plugin.Multilingual;
using Stuttering.Helper;
using Stuttering.Models;
using Stuttering.Resx;
using Stuttering.Services;
using Stuttering.SQL;
using Stuttering.ViewModels;
using Stuttering.Views.Assessment;
using Stuttering.Views.Management;
using Stuttering.Views.Popup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Evaluation = Stuttering.Views.Assessment.Evaluation;
using Rating = Stuttering.Views.Assessment.Rating;

namespace Stuttering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage : ContentPage
    {
        int id = 0;
        Metadata metadata;
        public MasterDetailPage()
        {
            //var culture = new CultureInfo(GlobalSettings.CurrentUser.Language);
            //Thread.CurrentThread.CurrentUICulture = culture;
            //CrossMultilingual.Current.CurrentCultureInfo = culture;
            //StutteringResources.Culture = culture;
            BindingContext = this;

            
            
            InitializeComponent();

            //var culture = new CultureInfo(GlobalSettings.CurrentUser.Language);
            ////CultureInfo.DefaultThreadCurrentCulture = culture;
            //StutteringResources.Culture = culture;
            //CrossMultilingual.Current.CurrentCultureInfo = culture;
            //if (StutteringResources.Culture.Name.ToUpper() != GlobalSettings.CurrentUser.Language.ToUpper())
            //{
            //    CultureInfo englishUSCulture = new CultureInfo(GlobalSettings.CurrentUser.Language);
            //    CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
            //    (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
            //}
            if (GlobalSettings.CurrentUser.Language == "ur")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                navigationDrawer.Position = Syncfusion.SfNavigationDrawer.XForms.Position.Right;
            }


            version.Text = Xamarin.Essentials.AppInfo.VersionString;
        }

        private void Overview_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.overview)
                return;
            id = 1;
            InfoBtn.IsVisible = false;
            NavTitle.Text = StutteringResources.overview;
            PageContent.Content = new StutteringOverviewPage();
            navigationDrawer.ToggleDrawer();
        }
        private void Management_Clicked(object sender, EventArgs e)
        {
            if (management.Text.Contains("+")){
                management.Text = StutteringResources.menu_management_close;
                flexible.IsVisible = true;
                onset.IsVisible = true;
                breathing.IsVisible = true;
            }
            else
            {
                //var NavTitle = appshell.CurrentPage.NavTitle;
                //if (NavTitle.ToLower().Contains("flexible") || NavTitle.ToLower().Contains("onset") || NavTitle.ToLower().Contains("breathing"))
                //    return;
                management.Text = StutteringResources.menu_management_open;
                flexible.IsVisible = false;
                onset.IsVisible = false;
                breathing.IsVisible = false;
            }
        }
        private void Breathing_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_breathing)
                return;
            id = 2;
            InfoBtn.IsVisible = false;
            NavTitle.Text = StutteringResources.title_breathing;
            PageContent.Content = new Breathing();
            navigationDrawer.ToggleDrawer();
        }
        private void Flexible_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_flexible)
                return;
            id = 3;
            InfoBtn.IsVisible = false;
            NavTitle.Text = StutteringResources.title_flexible;
            PageContent.Content = new Flexible();
            navigationDrawer.ToggleDrawer();
        }
        private void Onset_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_onset)
                return;
            id = 4;
            InfoBtn.IsVisible = false;
            NavTitle.Text = StutteringResources.title_onset;
            PageContent.Content = new Onset();
            navigationDrawer.ToggleDrawer();
        }
        private void Assessment_Clicked(object sender, EventArgs e)
        {
            if (assessment.Text.Contains("+"))
            {
                assessment.Text = StutteringResources.menu_assessment_close;
                rating.IsVisible = true;
                evaluation.IsVisible = true;
                rater.IsVisible = true;
            }
            else
            {
                //var NavTitle = appshell.CurrentPage.NavTitle;
                //if (NavTitle.ToLower().Contains("flexible") || NavTitle.ToLower().Contains("onset") || NavTitle.ToLower().Contains("breathing"))
                //    return;
                assessment.Text = StutteringResources.menu_assessment_open;
                rating.IsVisible = false;
                evaluation.IsVisible = false;
                rater.IsVisible = false;
            }
        }
        private void Rating_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_rating)
                return;
            id = 5;
            InfoBtn.IsVisible = true;
            NavTitle.Text = StutteringResources.title_rating;
            PageContent.Content = new Rating();
            navigationDrawer.ToggleDrawer();
        }
        private void Evaluation_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_evaluation)
                return;
            id = 6;
            InfoBtn.IsVisible = true;
            NavTitle.Text = StutteringResources.title_evaluation;
            PageContent.Content = new Evaluation();
            navigationDrawer.ToggleDrawer();
        }
        private void Rater_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_rater)
                return;
            id = 7;
            InfoBtn.IsVisible = true;
            NavTitle.Text = StutteringResources.title_rater;
            PageContent.Content = new Rater();
            navigationDrawer.ToggleDrawer();
        }
        private void Settings_Clicked(object sender, EventArgs e)
        {
            if (NavTitle.Text == StutteringResources.title_settings)
                return;
            id = 8;
            InfoBtn.IsVisible = false;
            NavTitle.Text = StutteringResources.title_settings;
            PageContent.Content = new Settings();
            navigationDrawer.ToggleDrawer();
        }
        private void Logout_Clicked(object sender, EventArgs e)
        {
            var quitApp = DependencyService.Get<IQuitAppService>();
            quitApp.Quit();
        }
        private void MenuToogle_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
        private async void InfoBtn_Clicked(object sender, EventArgs e)
        {
            switch (id)
            {
                case 5:
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new RatingInfoPopup(new Models.StutterReadItem()
                    {
                        Description = StutteringResources.self_rating_instruction,
                        Label = StutteringResources.instructions,
                        Type = Models.StutterreadItemType.Text
                    }));
                    break;
                case 6:
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new RatingInfoPopup(new Models.StutterReadItem()
                    {
                        Description = "1 = "+ StutteringResources.never + " \n 2 = "+ StutteringResources.occasionaly + " \n 3 = "+ StutteringResources.often + " \n 4 = " + StutteringResources.mostly,
                        Label = StutteringResources.instructions,
                        Type = Models.StutterreadItemType.Text
                    }));
                    break;
                case 7:
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new RatingInfoPopup(new Models.StutterReadItem()
                    {
                        Description = StutteringResources.rater_inst_start + "\n" +
                        StutteringResources.rater_inst_stop + "\n" +
                        StutteringResources.rater_inst_reset + "\n" +
                        StutteringResources.rater_inst_d + "\n" +
                        StutteringResources.rater_inst_s  ,
                        Label = StutteringResources.instructions,
                        Type = Models.StutterreadItemType.Text
                    }));
                    break;
                default:
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            metadata = SQLiteDbManager.GetMetadata();
            if (NavTitle.Text == StutteringResources.title_settings)
            {
                var settings = PageContent.Content as Settings;
                var vm = (settings as BindableObject).BindingContext as SettingsViewModel;
                if(vm != null)
                {
                    vm.OnAppearingCommand.Execute(this);
                }
            }
            if (NavTitle.Text == StutteringResources.title_flexible)
            {
                var flexible = PageContent.Content as Flexible;
                var vm = (flexible as BindableObject).BindingContext as FlexibleViewModel;
                if (vm != null)
                {
                    vm.OnAppearingCommand.Execute(this);
                }
            }
            if (NavTitle.Text == StutteringResources.title_onset)
            {
                var onset = PageContent.Content as Onset;
                var vm = (onset as BindableObject).BindingContext as OnsetViewModel;
                if (vm != null)
                {
                    vm.OnAppearingCommand.Execute(this);
                }
            }
        }
    }
}