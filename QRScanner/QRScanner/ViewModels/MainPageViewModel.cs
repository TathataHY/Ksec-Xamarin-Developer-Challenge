using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using QRScanner.Models;
using QRScanner.Views;
using Xamarin.Forms;

namespace QRScanner.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        public List<KSEC> KSEClist { get; set; }

        public ICommand GoReadQR { get; set; }
        public ICommand Delete { get; set; }

        public MainPageViewModel()
        {
            Refresh();

            GoReadQR = new Command(GoReadQRMethod);
            Delete = new Command((data) =>
            {
                DeleteMethod((KSEC)data);
                Refresh();
            });

            MessagingCenter.Subscribe<MainPage, ZXing.Result>(this, "NewKSEC", (a, s) =>
            {
                ScannerMethod(s);
            });
        }

        private void GoReadQRMethod()
        {
            MessagingCenter.Send(this, "GoToReadQR");
        }

        private void DeleteMethod(KSEC data)
        {
            App.KSECRepository.DeleteItem(data);
        }

        private void Refresh()
        {
            KSEClist = App.KSECRepository.GetItems();
        }

        private void ScannerMethod(ZXing.Result result)
        {
            KSEC nuevo = new KSEC();

            var resultA = StringCipher.Decrypt(result.Text).Replace("KSEC:", string.Empty).Replace(" ", string.Empty).Split(',');

            nuevo.name = resultA[0];
            nuevo.lastname = resultA[1];
            nuevo.rut = resultA[2];

            if (!KSEClist.Exists(x => x.name == nuevo.name && x.lastname == nuevo.lastname || x.rut == nuevo.rut))
            {
                App.KSECRepository.SaveItem(nuevo);
                Refresh();
            }
            else
            {
                MessagingCenter.Send(this, "AlreadyExists", nuevo);
            }
        }
    }
}
