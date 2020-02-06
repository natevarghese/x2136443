using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace x2136443.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        ICommand _MuteButtonClickedCommand;
        public ICommand MuteButtonClickedCommand => _MuteButtonClickedCommand ?? (_MuteButtonClickedCommand = new Command(MuteClicked));

        ICommand _TimeElapsedCommand;
        public ICommand TimeElapsedCommand => _TimeElapsedCommand ?? (_TimeElapsedCommand = new Command<int>(PlaybackTicked));

        string _Url;
        public string Url
        {
            get => _Url;
            set => SetProperty(ref _Url, value);
        }


        int _Volume;
        public int Volume
        {
            get => _Volume;
            set => SetProperty(ref _Volume, value);
        }

        public override void ViewModelAppearing()
        {
            base.ViewModelAppearing();

            Volume = 100;
        }

        void MuteClicked()
        {
            Volume = Volume == 0 ? 100 : 0;


        }

        public void PlaybackTicked(int seconds)
        {
            if (seconds < 0) return;

            var name = Url.Split('/').Last();
            App.PlaybackManager.UploadTimeForVideo(name, seconds);
        }
    }
}

