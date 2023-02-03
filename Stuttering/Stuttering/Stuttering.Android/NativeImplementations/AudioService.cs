using System;
using Xamarin.Forms;
using Stuttering.Droid;
using Android.Media;
using Stuttering.Helper;
using Stuttering.Droid.NativeImplementations;
using System.IO;
using Stream = Android.Media.Stream;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AudioSerivce))]
namespace Stuttering.Droid
{
    public class AudioSerivce : IAudio
    {
        MediaPlayer player;
        //StreamReader sr;
        public AudioSerivce()
        {
        }

        public async void Play(string url)
        {
            if (player != null && player.IsPlaying)
            {
                return;
            }
            if (player == null)
            {
                this.player = new MediaPlayer();
                //sr = new StreamReader(url);
                //player.SetDataSource(new StreamMediaDataSource(sr.BaseStream));
                //this.player.SetAudioStreamType(Stream.Music);

                player.SetAudioStreamType(Stream.Music);
                player.SetDataSource(Android.App.Application.Context, Android.Net.Uri.Parse(url));
                player.Prepare();

                //this.player.Prepare();
                this.player.Start();
            }
            else
            {
                this.player.Start();
            }
        }
        public void Pause()
        {
            if (player != null)
            {
                player.Pause(); 
            }
        }
        public void Stop()
        {
            try
            {
                if (player != null)
                {
                    this.player.Stop();
                    //sr.Close();
                    player = null;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}