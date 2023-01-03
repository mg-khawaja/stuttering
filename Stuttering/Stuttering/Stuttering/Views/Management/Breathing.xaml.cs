using Stuttering.Resx;
using Stuttering.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Breathing : ContentView, INotifyPropertyChanged
    {
        #region properties
        int durationTime = 1;
        Timer timer;
        string timerText = string.Empty;
        Stopwatch stopwatch;
        System.Threading.CancellationTokenSource cancellationSource = new System.Threading.CancellationTokenSource();
        public string TimerText
        {
            get { return timerText; }
            set { SetProperty(ref timerText, value); }
        }
        #endregion
        public Breathing()
        {
            BindingContext = this;
            stopwatch = new Stopwatch();
            timer = new Timer();
            timer.Interval = 200;
            InitializeComponent();
            picker.ItemsSource = new List<string>()
            {
                StutteringResources._1_min, StutteringResources._2_min,
                StutteringResources._3_min, StutteringResources._4_min,
                StutteringResources._5_min, StutteringResources._6_min,
                StutteringResources._7_min, StutteringResources._8_min,
                StutteringResources._9_min, StutteringResources._10_min,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            MessagingCenter.Subscribe<string>("animate", "animate", (obj) =>
            {
                animate();
            });
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var dur = (new DateTime(durationTime * 10000000 * 60)).ToString("HH:mm:ss");
                TimerText = (new DateTime(stopwatch.Elapsed.Ticks)).ToString("HH:mm:ss");
                if (TimerText == dur)
                {
                    ResetBtn_Clicked(null, null);
                }
                //if (StartStopBtn.Text == "Stop")
                //{
                ////}
                //if ((stopwatch.Elapsed.TotalMilliseconds / 60000) > 0)
                //{
                //    SyllablePerMinute = (Math.Round(syllablesInt / (stopwatch.Elapsed.TotalMilliseconds / 60000), 2)).ToString();
                //}
                //if (syllablesInt > 0)
                //{
                //    double dbl = stuttersInt / syllablesInt * 100;
                //    StutterPerSyllable = Math.Round(dbl, 1).ToString() + "%";
                //}
            }
            catch (Exception ex)
            {

            }

        }

        private async void animate()
        {
            try
            {
                cancellationSource = new System.Threading.CancellationTokenSource();
                var inhale = Convert.ToInt32(Inhale.Text);
                var exhale = Convert.ToInt32(Exhale.Text);
                var hold1 = Convert.ToInt32(Hold1.Text);
                var hold2 = Convert.ToInt32(Hold2.Text);
                if (!stopwatch.IsRunning)
                {
                    return;
                }
                BreathCircleText.Text = StutteringResources.breath_in + "\n" + inhale + " " + StutteringResources.secs;
                await BreathingCircle.ScaleTo(1.5, (uint)(inhale * 1000));
                BreathCircleText.Text = StutteringResources.hold_on + "\n" + hold1 + " " + StutteringResources.secs;
                if (!stopwatch.IsRunning)
                {
                    return;
                }
                else
                {
                    await Task.Delay(hold1 * 1000, cancellationSource.Token);
                }
                BreathCircleText.Text = StutteringResources.breath_out + "\n" + exhale + " " + StutteringResources.secs;
                await BreathingCircle.ScaleTo(1, (uint)(exhale * 1000));
                BreathCircleText.Text = StutteringResources.hold_on + "\n" + hold2 + " " + StutteringResources.secs;
                if (!stopwatch.IsRunning)
                {
                    return;
                }
                else
                {
                    await Task.Delay(hold2 * 1000, cancellationSource.Token);
                }
                BreathCircleText.Text = "";
                if (stopwatch.IsRunning)
                {
                    animate();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void InhaleMinus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Inhale.Text);
            if (val != 0)
                Inhale.Text = (--val).ToString();
        }

        private void InhalePlus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Inhale.Text);
            Inhale.Text = (++val).ToString();
        }

        private void Hold1Minus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Hold1.Text);
            if (val != 0)
                Hold1.Text = (--val).ToString();
        }

        private void Hold1Plus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Hold1.Text);
            Hold1.Text = (++val).ToString();
        }

        private void ExhaleMinus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Exhale.Text);
            if (val != 0)
                Exhale.Text = (--val).ToString();
        }

        private void ExhalePlus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Exhale.Text);
            Exhale.Text = (++val).ToString();
        }

        private void Hold2Minus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Hold2.Text);
            if (val != 0)
                Hold2.Text = (--val).ToString();
        }

        private void Hold2Plus_Clicked(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(Hold2.Text);
            Hold2.Text = (++val).ToString();
        }

        private void DefaultBtn_Clicked(object sender, EventArgs e)
        {
            if (DefaultBtn.Source == ImageSource.FromFile("defaultWhite.png"))
                return;
            DefaultBtn.Source = ImageSource.FromFile("defaultWhite.png");
            YogaBtn.Source = ImageSource.FromFile("yoga.png");
            SquareBtn.Source = ImageSource.FromFile("square.png");
            DefaultBtn.BackgroundColor = (Color)App.Current.Resources["Darker"];
            YogaBtn.BackgroundColor = Color.White;
            SquareBtn.BackgroundColor = Color.White;
            Inhale.Text = "4";
            Hold1.Text = "0";
            Exhale.Text = "6";
            Hold2.Text = "0";
        }

        private void YogaBtn_Clicked(object sender, EventArgs e)
        {
            if (YogaBtn.Source == ImageSource.FromFile("yogaWhite.png"))
                return;
            DefaultBtn.Source = ImageSource.FromFile("defaultDark.png");
            YogaBtn.Source = ImageSource.FromFile("yogaWhite.png");
            SquareBtn.Source = ImageSource.FromFile("square.png");
            DefaultBtn.BackgroundColor = Color.White;
            YogaBtn.BackgroundColor = (Color)App.Current.Resources["Darker"];
            SquareBtn.BackgroundColor = Color.White;
            Inhale.Text = "7";
            Hold1.Text = "4";
            Exhale.Text = "8";
            Hold2.Text = "0";
        }

        private void SquareBtn_Clicked(object sender, EventArgs e)
        {
            if (SquareBtn.Source == ImageSource.FromFile("squareWhite.png"))
                return;
            DefaultBtn.Source = ImageSource.FromFile("defaultDark.png");
            YogaBtn.Source = ImageSource.FromFile("yoga.png");
            SquareBtn.Source = ImageSource.FromFile("squareWhite.png");
            DefaultBtn.BackgroundColor = Color.White;
            YogaBtn.BackgroundColor = Color.White;
            SquareBtn.BackgroundColor = (Color)App.Current.Resources["Darker"];
            Inhale.Text = "4";
            Hold1.Text = "4";
            Exhale.Text = "4";
            Hold2.Text = "4";
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            resetBreath();
            PlayButtons.IsVisible = false;
            EditButtons.IsVisible = true;
            TickButtons.IsVisible = true;
        }

        private void PlayBtn_Clicked(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                return;
            }
            else
            {
                stopwatch.Start();
                animate();
                var meta = SQLiteDbManager.GetMetadata();
                if (meta != null)
                {
                    meta.BreathDateTime = DateTime.Now;
                    SQLiteDbManager.SaveMetadata(meta);
                }
            }
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            resetBreath();
            cancellationSource.Cancel();
        }
        private void resetBreath()
        {
            BreathingCircle.ScaleTo(1, 0);
            BreathCircleText.Text = "";
        }
        private void CloseBtn_Clicked(object sender, EventArgs e)
        {
            PlayButtons.IsVisible = true;
            EditButtons.IsVisible = false;
            TickButtons.IsVisible = false;
        }

        private void TickBtn_Clicked(object sender, EventArgs e)
        {
            PlayButtons.IsVisible = true;
            EditButtons.IsVisible = false;
            TickButtons.IsVisible = false;
        }

        private void start_Clicked(object sender, EventArgs e)
        {
            durationTime = Convert.ToInt32(picker.SelectedIndex) + 1;
            duration.IsVisible = false;
        }

        #region INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}