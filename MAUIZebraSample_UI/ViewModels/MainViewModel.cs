using Android.App;
using Android.Content;
using MAUIZebraSample_UI.Interfaces.Services;
using MAUIZebraSample_UI.Models;
using System.Windows.Input;

namespace MAUIZebraSample_UI.ViewModels
{
    public class MainViewModel : BindableObject
    {
        internal readonly Context Context = Android.App.Application.Context;


        private string _scannedText;
        public string ScannedText
        {
            get => _scannedText;
            set
            {
                _scannedText = value;
                OnPropertyChanged();
            }
        }

        private bool _showTestPrintButton;
        public bool ShowTestPrintButton
        {
            get => _showTestPrintButton;
            set
            {
                _showTestPrintButton = value;
                OnPropertyChanged();
            }
        }


        private string _pairingButtonText;
        public string PairingButtonText
        {
            get => _pairingButtonText;
            set
            {
                _pairingButtonText = value;
                OnPropertyChanged();
            }
        }


        private string _printerName = "YOUR_PRINTER_NAME_HERE";
        public string PrinterName
        {
            get => _printerName;
            set
            {
                _printerName = value;
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
        public ICommand OnToggleBluetoothPairingClicked { get; }
        public ICommand OnInvokeTestPrintClicked { get; }


        private readonly IScannerService _scannerService;
        private readonly IBluetoothService _bluetoothService;
        private Scanner _scanner;

        public MainViewModel(IScannerService scannerService, IBluetoothService bluetoothService)
        {
            _scannerService = scannerService;
            _bluetoothService = bluetoothService;
            _scannerService.Context = Context;
            _bluetoothService.Context = Context;

            _scanner = new Scanner(_scannerService, _bluetoothService);
            ToggleScannerButtonText = _scanner.ToggleScannerButtonText;
            ScannedText = _scanner.ScannedTest;
            _pairingButtonText = _scanner.PairingButtonText;
            _showTestPrintButton = _scanner.IsBluetoothPaired;

            OnToggleScannerClicked = new Command(_scanner.ToggleScanner);
            OnToggleBluetoothPairingClicked = new Command(async (x) => await _scanner.ToggleBluetoothPairing(_printerName));
            OnInvokeTestPrintClicked = new Command(_scanner.PrintTest);
            
            _scanner.PropertyChanged += Scanner_PropertyChanged;
        }

        private void Scanner_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is Scanner scanner)
            {
                switch (e.PropertyName)
                {
                    case nameof(Scanner.ScannedTest):
                        ScannedText = scanner.ScannedTest;
                        break;
                    case nameof(Scanner.ToggleScannerButtonText):
                        ToggleScannerButtonText = scanner.ToggleScannerButtonText;
                        break;
                    case nameof(Scanner.IsBluetoothPaired):
                    case nameof(Scanner.PairingButtonText):
                        PairingButtonText = scanner.PairingButtonText;
                        ShowTestPrintButton = scanner.IsBluetoothPaired;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
