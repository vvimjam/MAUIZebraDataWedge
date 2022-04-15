using Android.Content;
using MAUIZebraSample_UI.Interfaces.Services;
using MAUIZebraSample_UI.Models;
using System.Windows.Input;

namespace MAUIZebraSample_UI.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private string _scannedText;
        internal readonly Context Context = Android.App.Application.Context;

        public string ScannedText
        {
            get => _scannedText;
            set
            {
                _scannedText = value;
                OnPropertyChanged();
            }
        }

        private string _toggleScannerButtonText;

        public string ToggleScannerButtonText
        {
            get => _toggleScannerButtonText;
            set
            {
                _toggleScannerButtonText = value;
                OnPropertyChanged();
            }
        }

        public ICommand OnToggleScannerClicked { get; }

        private readonly IScannerService _scannerService;
        private Scanner _scanner;

        public MainViewModel(IScannerService scannerService)
        {
            _scannerService = scannerService;
            _scannerService.Context = Context;

            _scanner = new Scanner(_scannerService);
            ToggleScannerButtonText = _scanner.ToggleScannerButtonText;
            ScannedText = _scanner.ScannedTest;

            OnToggleScannerClicked = new Command(_scanner.ToggleScanner);
            _scanner.PropertyChanged += Scanner_PropertyChanged;
        }

        private void Scanner_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (sender is Scanner)
            {
                switch (e.PropertyName)
                {
                    case nameof(Scanner.ScannedTest):
                        ScannedText = ((Scanner)sender).ScannedTest;
                        break;
                    case nameof(Scanner.ToggleScannerButtonText):
                        ToggleScannerButtonText = ((Scanner)sender).ToggleScannerButtonText;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
