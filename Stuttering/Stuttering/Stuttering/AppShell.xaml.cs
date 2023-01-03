using Stuttering.Helper;
using Stuttering.ViewModels;
using Stuttering.Views;
using Stuttering.Views.Auth;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Stuttering
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            if (GlobalSettings.CurrentUser.Language == "ur")
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            version.Text = Xamarin.Essentials.AppInfo.VersionString;
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            (Application.Current as App).MainPage = new PinView(Helper.LockCheckType.Login);
        }

        private void ManagementMenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                switch (management.Text)
                {
                    case "Self Management     -":
                        var title = appshell.CurrentPage.Title;
                        if (title.ToLower().Contains("flexible") || title.ToLower().Contains("onset") || title.ToLower().Contains("breathing"))
                            return;
                        management.Text = "Self Management     +";
                        flexible.IsVisible = false;
                        onset.IsVisible = false;
                        breathing.IsVisible = false;
                        break;
                    case "Self Management     +":
                        management.Text = "Self Management     -";
                        flexible.IsVisible = true;
                        onset.IsVisible = true;
                        breathing.IsVisible = true;
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void AssesmentMenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                switch (assessment.Text)
                {
                    case "Self Assessment     -":
                        var title = appshell.CurrentPage.Title;
                        if (title.ToLower().Contains("rating") || title.ToLower().Contains("evaluation") || title.ToLower().Contains("rater"))
                            return;
                        assessment.Text = "Self Assessment     +";
                        rating.IsVisible = false;
                        evaluation.IsVisible = false;
                        rater.IsVisible = false;
                        break;
                    case "Self Assessment     +":
                        assessment.Text = "Self Assessment     -";
                        rating.IsVisible = true;
                        evaluation.IsVisible = true;
                        rater.IsVisible = true;
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
