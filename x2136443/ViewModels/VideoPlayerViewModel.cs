using System;

using Xamarin.Forms;

namespace x2136443.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        string _Url;
        public string Url
        {
            get => _Url;
            set => SetProperty(ref _Url, value);
        }

    }
}

