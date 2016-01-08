using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.ViewModel
{
    class ScanViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        string _mainText;

        public ScanViewModel()
        {
            _mainText = "Scan Page";
        }

        public string MainText
        {
            get { return _mainText; }
            set { SetProperty(ref _mainText, value, () => MainText); }
        }
    }
}
