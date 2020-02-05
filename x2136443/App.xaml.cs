using System;
using System.Threading.Tasks;
using x2136443.Pages;
using x2136443.Services;
using x2136443.Services.RemoveServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace x2136443
{
    public partial class App : Application
    {
        public static ISANSService SANSService => ServiceLocator.Instance.Resolve<ISANSService>();
        public static IDialogService DialogService => ServiceLocator.Instance.Resolve<IDialogService>();
        public static IPersistantStorageService PersistantStorageService => ServiceLocator.Instance.Resolve<IPersistantStorageService>();
        public static IPlaybackManager PlaybackManager => ServiceLocator.Instance.Resolve<IPlaybackManager>();

        public App()
        {
            InitializeComponent();

            ServiceLocator.Instance.Add<ISANSService, SANSService>();
            ServiceLocator.Instance.Add<IDialogService, DialogService>();
            ServiceLocator.Instance.Add<IPersistantStorageService, PersistantStorageService>();
            ServiceLocator.Instance.Add<IFileManager, FileManager>();
            ServiceLocator.Instance.Add<IPlaybackManager, PlaybackManager>();

            Akavache.Registrations.Start("x2136443");

            MainPage = new NavigationPage(new VideoListPage());
        }

        protected override void OnStart()
        {
            Subscribe();
        }
        protected override void OnResume()
        {
            Subscribe();
        }
        protected override void OnSleep()
        {
            Unsubscribe();
        }



        async void Subscribe()
        {
            await App.PlaybackManager.Start();
        }

        async void Unsubscribe()
        {
            await App.PlaybackManager.Save();
        }
    }
}
