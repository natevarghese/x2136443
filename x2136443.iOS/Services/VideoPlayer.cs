using System;
using System.Threading.Tasks;
using x2136443.Services;
using AVFoundation;
using AVKit;
using Foundation;
using UIKit;
using CoreMedia;
using x2136443.iOS.ViewControllers;

namespace x2136443.iOS.Services
{
    public class VideoPlayer : IVideoPlayer
    {
        public void PlayVideo(string url, int startOffset)
        {
            var nsurl = NSUrl.FromFilename(url);
            if (nsurl == null) return;

            var playerItem = AVPlayerItem.FromUrl(nsurl);
            if (playerItem == null) return;

            var vc = new TimeObservableAVPlayerViewController { Player = new AVPlayer(playerItem) };

            var root = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (vc.PresentedViewController != null)
                root = vc.PresentedViewController;

            root.PresentViewController(vc, true, () =>
            {
                if (startOffset <= vc.Player.CurrentItem.Duration.Seconds)
                    vc.Player.Seek(CMTime.FromSeconds(startOffset, 1), CMTime.Zero, CMTime.Zero);

                vc.Player.Play();
            });
        }
    }
}
