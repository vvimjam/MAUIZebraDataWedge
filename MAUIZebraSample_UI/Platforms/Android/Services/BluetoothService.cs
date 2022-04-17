using Android.Bluetooth;
using Android.Content;
using Java.Util;
using MAUIZebraSample_UI.Interfaces.Services;

namespace MAUIZebraSample_UI.Platforms.Android.Services
{
    public class BluetoothService : IBluetoothService
    {
        BluetoothAdapter _bluetoothAdapter;
        const string SPP = "00001101-0000-1000-8000-00805f9b34fb";

        Context _context;
        public Context Context { set => _context = value; }

        private bool _isConnectionInProgress = false;


        public BluetoothService()
        {
            GetBluetoothAdapter();
        }

        public bool IsBluetoothEnabled()
        {
            return _bluetoothAdapter == null ? false : _bluetoothAdapter.IsEnabled;
        }

        public bool IsBluetoothDeviceConnected(string deviceName)
        {
            if (_bluetoothAdapter == null || _bluetoothAdapter.BondedDevices == null)
                return false;

            return _bluetoothAdapter.BondedDevices.Count(x => x.Name == deviceName) > 0;
        }

        public List<string> GetAvailableBluetoothDevices()
        {
            if (IsBluetoothEnabled())
                return _bluetoothAdapter.BondedDevices.Select(d => d.Name).ToList();

            return new List<string>();
        }

        public async Task ConnectPairedDevices(string deviceName)
        {

            if (!IsBluetoothEnabled())
                throw new Exception($"Bluetooth not enabled.");

            var vs = _bluetoothAdapter.BondedDevices.ToList();

            var device = _bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);

            if (device == null)
                throw new Exception($"Bluetooth device {deviceName} not found.");

            var _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString(SPP));

            _isConnectionInProgress = true;

            await _socket.ConnectAsync().ContinueWith(x => _isConnectionInProgress = false);
        }

        //Note: This is a hidden api. Might get depricated/inaccessible later on.
        public void PairDevice(string deviceName)
        {
            //Unsure on details to get bluetooth device details without pairing.
            BluetoothDevice device = null;

            var mi = device.Class.GetMethod("createBond", null);
            mi.Invoke(device, null);
        }

        //Note: This is a hidden api. Might get depricated/inaccessible later on.
        public void RemovePairedDevice(string deviceName)
        {
            var device = _bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);

            if (device != null)
            {
                var mi = device.Class.GetMethod("removeBond", null);
                mi.Invoke(device, null);
            }
        }


        private void GetBluetoothAdapter()
        {
            try
            {
                BluetoothManager bluetoothManager = _context.GetSystemService((string)Context.BluetoothService) as BluetoothManager;
                _bluetoothAdapter = bluetoothManager.Adapter;
            }
            catch (Exception)
            {
                _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            }
        }

    }
}
