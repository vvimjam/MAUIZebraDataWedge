using Android.Bluetooth;
using Android.Content;

namespace MAUIZebraSample_UI.Interfaces.Services
{
    public interface IBluetoothService
    {
        Context Context { set; }

        bool IsBluetoothEnabled();

        bool IsBluetoothDeviceConnected(string deviceName);

        List<string> GetAvailableBluetoothDevices();

        Task<string> ConnectPairedDevices(string deviceName);

        void PairDevice(string deviceName);

        void RemovePairedDevice(string deviceName);

        string GetConnectedBluetoothDeviceAddress(string deviceName);
    }
}
