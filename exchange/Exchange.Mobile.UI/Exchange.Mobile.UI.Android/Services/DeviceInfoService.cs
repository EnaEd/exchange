using Android.Content;
using Android.Telephony;
using Exchange.Mobile.Core.Services.Interfaces;

namespace Exchange.Mobile.UI.Droid.Services
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public string GetPhoneNumber()
        {
            TelephonyManager manager = Android.App.Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;
            return manager.Line1Number;
        }
    }
}
