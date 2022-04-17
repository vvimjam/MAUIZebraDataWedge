using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using MAUIZebraSample_UI.Platforms.Android.Services;

namespace MAUIZebraSample_UI;

[Activity(Label = "DataWedge_Intent_Example_4", Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    readonly Context _context = Android.App.Application.Context;

    public MainActivity()
    {
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        GetBluetoothPermissions();
    }



    //Permission request & check has to be within activity. Putting it in seperate service & passign in activity & context see to cause runtime issues.
    #region Bluetooth permission

    public const int AccessCoarseLocationPermissionRequestCode = 0;


    bool HasLocaionPermission()
    {
        return ContextCompat.CheckSelfPermission(_context, Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
    }

    bool HasBluetoothPermissions()
    {
        return ContextCompat.CheckSelfPermission(_context, Manifest.Permission.Bluetooth) == Permission.Granted
            && ContextCompat.CheckSelfPermission(_context, Manifest.Permission.BluetoothAdmin) == Permission.Granted;
    }

    void GetBluetoothPermissions()
    {
        if (HasLocaionPermission() && HasBluetoothPermissions())
            return;


        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.SetTitle("Permission Required")
            .SetMessage("Coarse location permission is required to perform bluetooth discovery function properly. Please accept this permission to allow Bluetooth discovery.")
            .SetPositiveButton("OK", OnPermissionRequiredDialogOkClicked)
            .SetCancelable(false)
            .Show();

        return;
    }

    void OnPermissionRequiredDialogOkClicked(object sender, DialogClickEventArgs e)
    {
        ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.Bluetooth, Manifest.Permission.BluetoothAdmin }, AccessCoarseLocationPermissionRequestCode);
    }
    #endregion

}
