using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Stuttering.Droid.NativeImplementations;
using Stuttering.Droid.SQLite;
using Stuttering.Models;
using System.Globalization;
using Plugin.Multilingual;
using MediaManager;
using AndroidX.AppCompat.App;

namespace Stuttering.Droid
{
    [Activity(Label = "Stuttering", Icon = "@drawable/ColorLogo", Theme = "@style/MainTheme", 
        MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            CrossMediaManager.Current.Init(this);
            DependencyService.Register<QuitAppService>();
            Instance = this;
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var sql = new SQLiteDroid();
            var con = sql.GetConnection();
            con.CreateTable<UserModel>();
            var user = con.Table<UserModel>().FirstOrDefault();

            if(user != null && user.Language == "ur")
            {
                var culture = new CultureInfo(user.Language);
                //CultureInfo.DefaultThreadCurrentCulture = culture;
                //StutteringResources.Culture = culture;
                CrossMultilingual.Current.CurrentCultureInfo = culture;

                var locale = new Java.Util.Locale(user.Language);
                Java.Util.Locale.Default = locale;

                var config = new Android.Content.Res.Configuration { Locale = locale };
                this.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);
            }

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}