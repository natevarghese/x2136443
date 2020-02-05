using System;
using System.Linq;
using x2136443.ViewModels;

namespace x2136443.Pages
{
    public partial class VideoPlayerPage : BasePage<VideoPlayerViewModel>
    {
        public string Url;
        public int StartTime;


        public VideoPlayerPage()
        {
            InitializeComponent();
        }


        protected override void PassDataToViewModel()
        {
            base.PassDataToViewModel();

            ViewModel.Url = Url;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ApplySafeAreaGridConstraints(ParentGrid);

            MyVideoPlayer.Volume = 100;

            MyVideoPlayer.PlayerStateChanged += MyVideoPlayer_PlayerStateChanged;
            MyVideoPlayer.TimeElapsed += MyVideoPlayer_TimeElapsed;
            MuteButton.Clicked += MuteButton_Clicked;
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MyVideoPlayer.PlayerStateChanged -= MyVideoPlayer_PlayerStateChanged;
            MyVideoPlayer.TimeElapsed -= MyVideoPlayer_TimeElapsed;
            MuteButton.Clicked -= MuteButton_Clicked;
        }


        async void MyVideoPlayer_PlayerStateChanged(object sender, Octane.Xamarin.Forms.VideoPlayer.Events.VideoPlayerStateChangedEventArgs e)
        {
            if (e.CurrentState == Octane.Xamarin.Forms.VideoPlayer.Constants.PlayerState.Prepared)
            {
                MyVideoPlayer.Play();
            }

            if (e.CurrentState == Octane.Xamarin.Forms.VideoPlayer.Constants.PlayerState.Playing)
            {
                //HACK delay required for some reason. Need more time 
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));

                MyVideoPlayer.Seek(StartTime);
            }
        }
        void MyVideoPlayer_TimeElapsed(object sender, Octane.Xamarin.Forms.VideoPlayer.Events.VideoPlayerEventArgs e)
        {
            var name = Url.Split('/').Last();
            App.PlaybackManager.UploadTimeForVideo(name, (int)e.CurrentTime.TotalSeconds);
        }

        void MuteButton_Clicked(object sender, EventArgs e)
        {
            MyVideoPlayer.Volume = MyVideoPlayer.Volume == 0 ? 100 : 0;
        }
    }
}
