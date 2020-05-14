using Android.App;
using Android.Widget;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Platforms.Android;
using System;

namespace Exchange.Mobile.UI.Droid.Services
{
    public class DisplayAlertService : IDisplayAlertService
    {
        public void ShowAlert(string message, string title, string okbtnText, Action okBtnAction)
        {
            var alert = new AlertDialog.Builder(GetCurrentActivity());
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton(okbtnText, (sender, args) =>
            {
                if (okBtnAction != null)
                {
                    okBtnAction.Invoke();
                }
            });
            alert.Create().Show();
        }

        public void ShowToast(string message)
        {
            var activity = GetCurrentActivity();
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(activity, message, ToastLength.Long).Show();
            });
        }

        private Activity GetCurrentActivity()
        {
            var top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            return top.Activity;
        }
    }
}
