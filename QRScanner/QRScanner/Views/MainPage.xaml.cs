using QRScanner.Models;
using QRScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace QRScanner.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MainPageViewModel>(this, "GoToReadQR", async (a) =>
            {
                var scan = new ZXingScannerPage();
                await Navigation.PushModalAsync(scan);

                scan.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopModalAsync();
                        MessagingCenter.Send(this, "NewKSEC", result);
                    });
                };
            });

            MessagingCenter.Subscribe<MainPageViewModel, KSEC>(this, "AlreadyExists", async (a, s) =>
            {
                await DisplayAlert("The user is already exists.", "User: " + s.name + " " + s.lastname + Environment.NewLine
                    + "Rut: " + s.rut, "Ok");
            });
        }
    }
}
