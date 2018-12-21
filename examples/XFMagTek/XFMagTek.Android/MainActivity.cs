
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XFMagTek.Droid
{
    [Activity(Label = "XFMagTek", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            // MagTek Card Reader
            CheckPermissions();
            MagTekApi.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

        }

        private readonly string[] Permissions =
        {
            Android.Manifest.Permission.Bluetooth,
            Android.Manifest.Permission.BluetoothAdmin,
            Android.Manifest.Permission.BluetoothPrivileged,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.AccessFineLocation
        };
        private void CheckPermissions()
        {
            bool minimumPermissionsGranted = true;

            foreach (string permission in Permissions)
            {
                if (CheckSelfPermission(permission) != Permission.Granted) 
                {
                    minimumPermissionsGranted = false;
                } 
            }

            // If one of the minimum permissions aren't granted, we request them from the user
            //if (!minimumPermissionsGranted)
            //{
                RequestPermissions(Permissions, 0);
            //}
        }
    }
}