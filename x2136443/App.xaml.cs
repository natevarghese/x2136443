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

        public App()
        {
            InitializeComponent();

            ServiceLocator.Instance.Add<ISANSService, SANSService>();
            ServiceLocator.Instance.Add<IDialogService, DialogService>();

            MainPage = new VideoListPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
