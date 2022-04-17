using MAUIZebraSample_UI.Interfaces.Services;

namespace MAUIZebraSample_UI.Models
{
    public class Scanner : BaseModel
    {
        bool isScannerEnabled = false;
        bool isBluetoothPaired = false;

        private readonly IScannerService _scannerService;
        private readonly IBluetoothService _bluetoothService;

        public string ToggleScannerButtonText => isScannerEnabled ? "Disable Scanner" : "Enable Scanner";

        public string PairingButtonText => isBluetoothPaired ? "Unpair vluetooth device" : "Pair bluetooth device";


        public string ScannedTest { get; private set; } = "Scanned Text";


        public Scanner(IScannerService scannerService,  IBluetoothService bluetoothService)
        {
            _scannerService = scannerService;
            _bluetoothService = bluetoothService;
        }

        public void ToggleScanner()
        {
            _scannerService.ToggleTrigger(!isScannerEnabled);
            SetProperty(ref isScannerEnabled, !isScannerEnabled, nameof(ToggleScannerButtonText));
        }

        public void ToggleBluetoothPairing(string printerName)
        {
            if (!_bluetoothService.IsBluetoothDeviceConnected(printerName))
            {
                _bluetoothService.ConnectPairedDevices(printerName);
            }

            SetProperty(ref isBluetoothPaired, !isBluetoothPaired, nameof(PairingButtonText));
        }
    }
}
