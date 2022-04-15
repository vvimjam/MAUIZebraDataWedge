using MAUIZebraSample_UI.Interfaces.Services;

namespace MAUIZebraSample_UI.Models
{
    public class Scanner : BaseModel
    {
        bool isScannerEnabled = false;
        private readonly IScannerService _scannerService;

        public string ToggleScannerButtonText => isScannerEnabled ? "Disable Scanner" : "Enable Scanner";

        public string ScannedTest { get; private set; } = "Scanned Text";


        public Scanner(IScannerService scannerService)
        {
            _scannerService = scannerService;
        }

        public void ToggleScanner()
        {
            _scannerService.ToggleTrigger(!isScannerEnabled);
            SetProperty(ref isScannerEnabled, !isScannerEnabled, nameof(ToggleScannerButtonText));
        }
    }
}
