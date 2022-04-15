using Android.Content;
using MAUIZebraSample_UI.Interfaces.Services;

namespace MAUIZebraSample_UI.Platforms.Android.Services
{
    public class ScannerService : IScannerService
    {
        private string DATAWEDGE_ACTION = "com.symbol.datawedge.api.ACTION";
        private string DATAWEDGE_EXTRA_KEY_SCANNER_CONTROL = "com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN";

        Context _context;
        public Context Context { set => _context = value; }


        public ScannerService()
        {

        }

        public void ToggleTrigger(bool enable = true)
        {
            Intent intent = new Intent();
            intent.SetAction(DATAWEDGE_ACTION);
            intent.PutExtra(DATAWEDGE_EXTRA_KEY_SCANNER_CONTROL, enable ? "ENABLE_PLUGIN" : "DISABLE_PLUGIN");
            _context.SendBroadcast(intent);

            Intent inten = new Intent();
            inten.SetAction("com.example.broadcast.MY_NOTIFICATION");
            inten.PutExtra("data", "Nothing to see here, move along.");
            _context.SendBroadcast(inten);
        }

    }
}
