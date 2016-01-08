using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Training.ViewModel
{
    class MainViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private string _mainText;

        public MainViewModel()
        {
            _mainText = "Scanner Application";
            ScanPage = new Command(() => Navigation.PushAsync<ScanViewModel>());
            MapPage = new Command(() => Navigation.PushAsync<MapViewModel>());
        }

        public string MainText
        {
            get { return _mainText; }
            set { SetProperty(ref _mainText, value, () => MainText); }
        }

        public Command ScanPage { get; set; }
        public Command MapPage { get; set; }
    }
}
