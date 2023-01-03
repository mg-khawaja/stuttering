using Stuttering.Models;
using Stuttering.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelfRatingStatsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public List<Rating> Data { get; set; }
        public SelfRatingStatsPopup()
        {
            try
            {
                BindingContext = this;
                Data = SQLiteDbManager.GetAllSelfRatingRecords();
                //var list = Data;

                //if (list != null && list.Count > 0)
                //{
                //    Maximum = list.Max(d => d.Syllables);
                //    var max = list.Max(d => d.Stutters);
                //    if (max > Maximum)
                //    {
                //        Maximum = max;
                //    }

                //    SPS_Maximum = list.Max(d => d.SyllablePerMinute);
                //    var sps_max = list.Max(d => d.StutterPerSyllable);
                //    if (sps_max > SPS_Maximum)
                //    {
                //        SPS_Maximum = sps_max;
                //    } 
                //}
                InitializeComponent();
                //if(SyllableAxis.Maximum > StutterAxis.Maximum)
                //{
                //    StutterAxis.Maximum = SyllableAxis.Maximum;
                //}
                //else
                //{
                //    SyllableAxis.Maximum = StutterAxis.Maximum;
                //}
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}