using MAUIZebraSample_UI.Interfaces.Services;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace MAUIZebraSample_UI.Platforms.Android.Services
{
    public class PrintingService : IPrintingService
    {
        const string instruction = "^XA^FO50,50^ADN,36,20^FDhelloworld^FS^XZ";

        public void Print(string deviceAddress)
        {
            //Open connection with printer
            var connection = ConnectionBuilder.Build("BT:" + deviceAddress);
            connection.Open();

            //Always pass the language type
            var printerInstance = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);

            //TODO: Perform other checks like connection status & printer readiness

            byte[] bytBuffer = System.Text.Encoding.GetEncoding(0).GetBytes(instruction);

            connection.Write(bytBuffer);
        }
    }
}
