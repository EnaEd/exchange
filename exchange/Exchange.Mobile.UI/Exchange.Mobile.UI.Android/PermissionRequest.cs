using System.Collections.Generic;

namespace Exchange.Mobile.UI.Droid
{
    public class PermissionRequest : Xamarin.Essentials.Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new List<(string androidPermission, bool isRuntime)>
    {
        (Android.Manifest.Permission.ReadPhoneState, true),
        (Android.Manifest.Permission.ReadPhoneNumbers, true),
        (Android.Manifest.Permission.AccessFineLocation, true),
        (Android.Manifest.Permission.AccessCoarseLocation, true),
        (Android.Manifest.Permission.AccessNetworkState,true),
        (Android.Manifest.Permission.ReadExternalStorage,true),
        (Android.Manifest.Permission.ReadContacts,true),
        (Android.Manifest.Permission.Internet,true),
        (Android.Manifest.Permission.BindTextService,true),
        (Android.Manifest.Permission.ChangeNetworkState,true),
        (Android.Manifest.Permission.WriteExternalStorage,true)
    }.ToArray();
    }
}
