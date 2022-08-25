using QRScanner.Models;
using QRScanner.Repositories;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRScanner
{
    public partial class App : Application
    {
        private static BaseRepository<KSEC> _ksecRepository;

        public static BaseRepository<KSEC> KSECRepository
        {
            get
            {
                if (_ksecRepository == null)
                {
                    _ksecRepository = new BaseRepository<KSEC>();
                }

                return _ksecRepository;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
