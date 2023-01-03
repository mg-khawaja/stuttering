using System;
using Stuttering.iOS;
using AVFoundation;
using Foundation;

using Xamarin.Forms;
using Stuttering.Helper;
using System.IO;

[assembly: Dependency(typeof(AudioService))]
namespace Stuttering.iOS
{
    public class AudioService : IAudio
    {
        int clicks = 0;

        public AudioService() { }
        AVPlayer player;
        public void Play(string url)
        {
            if (player != null && (player.Rate != 0) && (player.Error == null))
            {
                return;
            }
            if (player == null)
            {
                this.player = new AVPlayer();
                this.player = AVPlayer.FromUrl(NSUrl.FromString(url));
                this.player.Play();
            }
            else
            {
                this.player.Play();
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
                    player.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}