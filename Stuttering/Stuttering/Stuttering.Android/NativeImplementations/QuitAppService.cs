using Android.App;
using Stuttering.Services;

namespace Stuttering.Droid.NativeImplementations
{
    public class QuitAppService: IQuitAppService
    {
        public void Quit()
        {
            var activity = MainActivity.Instance as Activity;
            activity.Finish();
        }
    }
}