using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace Training.ViewModel
{
    class ScanViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        string _mainText;

        public ScanViewModel()
        {
            _mainText = "Scan Page";
            Scan = new Command(async () => await ScanCode());
        }

        public string MainText
        {
            get { return _mainText; }
            set { SetProperty(ref _mainText, value, () => MainText); }
        }

        public async Task ScanCode()
        {
            var scanner = new MobileBarcodeScanner();
            scanner.UseCustomOverlay = false;
            scanner.TopText = "Scan barcode";
            scanner.BottomText = "Back to cancel";

            var result = await scanner.Scan();
            ScanResultHandler(result);
        }

        void ScanResultHandler(ZXing.Result result)
        {
            string message = "";
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                message = result.Text;
            }
            else
            {
                message = "Scan cancelled";
            }
            MainText = message;
            if (result != null && String.Equals(result.BarcodeFormat.ToString(), "PDF_417", StringComparison.OrdinalIgnoreCase))
            {
                string[] arr = result.Text.Split('%');
                string antwoord;
                antwoord = "Make: " + arr[9] + "\nModel: " + arr[10] + "\nCar Description: " + arr[8] + "\nCar Colour: " + arr[11] + "\nLicense number: " + arr[6] + "\nDate of Expiry: " + arr[14];
                MainText = antwoord;
            }
        }

        public Command Scan { get; set; }
    }
}
