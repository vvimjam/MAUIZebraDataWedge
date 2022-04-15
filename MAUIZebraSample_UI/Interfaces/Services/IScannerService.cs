using Android.Content;

namespace MAUIZebraSample_UI.Interfaces.Services
{
    public interface IScannerService
    {
        Context Context { set; }

        void ToggleTrigger(bool enable = true);
    }
}
