using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.MagTek.Forms;
using Xamarin.MagTek.Forms.Models;
using XFMagTek.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFMagTek
{
    public partial class App : Application
    {
        public static App Instance;
        private IeDynamoService _cardReaderService = DependencyService.Get<IeDynamoService>();

        internal readonly IMagTekFactoryService MagTekFactory;

        public App()
        {
            Instance = this;
            InitializeComponent();

            MagTekFactory = new MagTekDeviceFactory(_cardReaderService);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
