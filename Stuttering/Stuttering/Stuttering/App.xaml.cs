using Plugin.Multilingual;
using SQLite;
using Stuttering.Helper;
using Stuttering.Resx;
using Stuttering.Services;
using Stuttering.SQL;
using Stuttering.Views;
using Stuttering.Views.Assessment;
using Stuttering.Views.Auth;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering
{
    public partial class App : Application
    {
        public bool IsStartup = true;
        public App()
        {
            SQLiteDbManager.Initialise();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjkxMDQwQDMyMzAyZTMxMmUzMFdneGlSY3pSeEwvVS84Uldrb3lHcFVJYVczOEkvM01JQUJUdlU3QWk4MFk9");
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            var user = SQLiteDbManager.GetUser();
            if (user != null && user.Language == "ur")
            {
                CultureInfo englishUSCulture = new CultureInfo(user.Language);
                CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
            }

            //TestPage
            //MainPage = new Evaluation();

            //Actual
            Xamarin.Essentials.VersionTracking.Track();
            if (Xamarin.Essentials.VersionTracking.IsFirstLaunchEver)
                //if (true)
                MainPage = new OnBoarding();
            else
                MainPage = new PinView(Helper.LockCheckType.Login);
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
