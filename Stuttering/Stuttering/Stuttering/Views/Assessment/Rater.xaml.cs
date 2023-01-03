using Stuttering.Resx;
using Stuttering.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Assessment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rater : ContentView, INotifyPropertyChanged
    {
        #region properties
        Timer timer;
        string timerText = string.Empty;
        string stutterPerSyllable = "0.0%";
        string syllablePerMinute = "0.00";
        float syllablesInt = 0;
        float stuttersInt = 0;
        string syllables = "0";
        string stutters = "0";
        Stopwatch stopwatch;
        public string TimerText
        {
            get { return timerText; }
            set { SetProperty(ref timerText, value); }
        }
        public string StutterPerSyllable
        {
            get { return stutterPerSyllable; }
            set { SetProperty(ref stutterPerSyllable, value); }
        }
        public string SyllablePerMinute
        {
            get { return syllablePerMinute; }
            set { SetProperty(ref syllablePerMinute, value); }
        }
        public string Syllables
        {
            get { return syllables; }
            set { SetProperty(ref syllables, value); }
        }
        public string Stutters
        {
            get { return stutters; }
            set { SetProperty(ref stutters, value); }
        }
        #endregion
        public Rater()
        {
            BindingContext = this;
            stopwatch = new Stopwatch();
            timer = new Timer();
            timer.Interval = 300;
            InitializeComponent();
            CheckMetadata();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                TimerText = new DateTime(stopwatch.Elapsed.Ticks).ToString("HH:mm:ss.ff");
                //if (StartStopBtn.Text == "Stop")
                //{


                //}
                if ((stopwatch.Elapsed.TotalMilliseconds / 60000) > 0)
                {
                    SyllablePerMinute = (Math.Round(syllablesInt / (stopwatch.Elapsed.TotalMilliseconds / 60000), 2)).ToString();
                }
                if (syllablesInt > 0)
                {
                    double dbl = stuttersInt / syllablesInt * 100;
                    StutterPerSyllable = Math.Round(dbl, 1).ToString() + "%";
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            StartStopBtn.Text = "Start";
            StutterPerSyllable = "0.0%";
            SyllablePerMinute = "0.00";
            syllablesInt = 0;
            stuttersInt = 0;
            Syllables = "0";
            Stutters = "0";
        }

        private void SyllableBtn_Clicked(object sender, EventArgs e)
        {
            Syllables = (++syllablesInt).ToString();
        }

        private void StutterBtn_Clicked(object sender, EventArgs e)
        {
            Stutters = (++stuttersInt).ToString(); ;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (TimerText == "00:00:00.00")
            {
                (Application.Current as App).MainPage.DisplayAlert(StutteringResources.error, StutteringResources.start_stutter, StutteringResources.ok);
            }
            else if (StartStopBtn.Text == "Stop")
            {
                (Application.Current as App).MainPage.DisplayAlert(StutteringResources.error, StutteringResources.stop_stutter, StutteringResources.ok);
            }
            else
            {
                double SPS = stuttersInt / syllablesInt * 100;
                SPS = Math.Round(SPS, 1);
                double SPM = Math.Round(syllablesInt / (stopwatch.Elapsed.TotalMilliseconds / 60000), 2);
                SQLiteDbManager.SaveStutterRaterRecord(new Models.StutterRaterRecord()
                {
                    Date = DateTime.Now.ToString("dd MMM"),
                    Duration = TimerText,
                    Stutters = Convert.ToInt32(Stutters),
                    Syllables = Convert.ToInt32(Syllables),
                    StutterPerSyllable = SPS,
                    SyllablePerMinute = SPM
                });
                var meta = SQLiteDbManager.GetMetadata();
                meta.ChNo_WhenAssessmentLocked = meta.FlexibleChapter + meta.OnsetChapter;
                SQLiteDbManager.SaveMetadata(meta);
                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
            }
        }
        private async void CheckMetadata()
        {
            var meta = SQLiteDbManager.GetMetadata();
            if(meta.ChNo_WhenAssessmentLocked != null && meta.ChNo_WhenAssessmentLocked >= meta.OnsetChapter + meta.FlexibleChapter)
            {
                await (Application.Current as App).MainPage.DisplayAlert("Alert", "Please complete at least one chapter from Flexible rate of speech or Easy Onset to access Statter Rater!", "OK");
                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
            }
            else
            {
                timer.Elapsed += Timer_Elapsed; ;
                timer.Start();
            }
        }
        private void StartStopBtn_Clicked(object sender, EventArgs e)
        {

            if (StartStopBtn.Text == "Start")
            {
                stopwatch.Start();
                StartStopBtn.Text = "Stop";
            }
            else
            {
                stopwatch.Stop();
                StartStopBtn.Text = "Start";
            }
        }
    }
}