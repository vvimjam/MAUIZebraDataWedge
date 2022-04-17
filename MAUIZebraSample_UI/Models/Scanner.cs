using MAUIZebraSample_UI.Interfaces.Services;
using MAUIZebraSample_UI.Platforms.Android.Services;

namespace MAUIZebraSample_UI.Models
{
    public class Scanner : BaseModel
    {
        bool isScannerEnabled = false;
        bool isBluetoothPaired = false;
        string _connectedDeviceAddress = string.Empty;
        PrintingService _printingService;

        private readonly IScannerService _scannerService;
        private readonly IBluetoothService _bluetoothService;

        public bool IsBluetoothPaired => isBluetoothPaired;
        public string ScannedTest { get; private set; } = "Scanned Text";
        public string ToggleScannerButtonText => isScannerEnabled ? "Disable Scanner" : "Enable Scanner";
        public string PairingButtonText => "Pair bluetooth device";


        public Scanner(IScannerService scannerService, IBluetoothService bluetoothService)
        {
            _scannerService = scannerService;
            _bluetoothService = bluetoothService;
        }

        public void ToggleScanner()
        {
            _scannerService.ToggleTrigger(!isScannerEnabled);
            SetProperty(ref isScannerEnabled, !isScannerEnabled, nameof(ToggleScannerButtonText));
        }

        public async Task ToggleBluetoothPairing(string printerName)
        {
            if (!_bluetoothService.IsBluetoothDeviceConnected(printerName))
            {
                _connectedDeviceAddress = await _bluetoothService.ConnectPairedDevices(printerName);
            }
            else
            {
                _connectedDeviceAddress = _bluetoothService.GetConnectedBluetoothDeviceAddress(printerName);
            }

            if (!string.IsNullOrEmpty(_connectedDeviceAddress))
            {
                SetProperty(ref isScannerEnabled, !isScannerEnabled, nameof(PairingButtonText));
                SetProperty(ref isBluetoothPaired, !isBluetoothPaired, nameof(IsBluetoothPaired));
            }
        }

        public void PrintTest()
        {
            if (!string.IsNullOrEmpty(_connectedDeviceAddress))
            {
                if (_printingService == null)
                    _printingService = new PrintingService();

                _printingService.Print(_connectedDeviceAddress);
            }
        }
    }
}
