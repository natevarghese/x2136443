using System;
using System.Linq;
using AVFoundation;
using AVKit;
using CoreMedia;

namespace x2136443.iOS.ViewControllers
{
    public class TimeObservableAVPlayerViewController : AVPlayerViewController
    {
        IDisposable TimeObservableToken;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            TimeObservableToken = Player.AddPeriodicTimeObserver(CMTime.FromSeconds(1, 1), null, TimeObserved);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            TimeObservableToken?.Dispose();
        }

        void TimeObserved(CMTime time)
        {
            if (Player.CurrentItem.Asset is AVUrlAsset asset)
            {
                var name = asset.Url.PathComponents.Last();
                App.PlaybackManager.UploadTimeForVideo(name, (int)time.Seconds);
            }
        }

#if DEBUG
        ~TimeObservableAVPlayerViewController()
        {
            System.Diagnostics.Debug.WriteLine("Finalizer called: " + GetType().Name);
        }
#endif
    }
}
