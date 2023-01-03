using Stuttering.Droid.SQLite;
using Stuttering.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace Stuttering.Droid.Locale
{
    public class LocalisationDroid : ILocalisation
    {
        public void ChangeLocale(string cultureName)
        {
            //var locale = new Java.Util.Locale(cultureName);
            //Java.Util.Locale.Default = locale;

            //var config = new Android.Content.Res.Configuration { Locale = locale };
            //CurrentActity.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);
        }
    }
}