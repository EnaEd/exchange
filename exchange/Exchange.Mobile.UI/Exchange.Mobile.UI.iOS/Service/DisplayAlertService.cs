using Exchange.Mobile.Core.Services.Interfaces;
using Foundation;
using System;
using UIKit;

namespace Exchange.Mobile.UI.iOS.Service
{
    public class DisplayAlertService : IDisplayAlertService
    {
        public void ShowAlert(string message, string title, string okbtnText, Action okBtnAction)
        {
            var alert = new UIAlertView()
            {
                Title = title,
                Message = message
            };
            alert.AddButton(okbtnText);

            alert.Clicked += (object s, UIButtonEventArgs e) =>
            {
                if (e.ButtonIndex == 0)
                {
                    okBtnAction?.Invoke();
                }
            };
            alert.Show();
        }

        public void ShowToast(string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView()
                {
                    Message = message,
                    Alpha = 1.0f
                };
                alert.Frame = new CoreGraphics.CGRect(alert.Frame.X, UIScreen.MainScreen.Bounds.Y * 0.7f, alert.Frame.Width, alert.Frame.Height);
                NSTimer tmr;
                alert.Show();

                tmr = NSTimer.CreateTimer(1.5, delegate
                {
                    alert.DismissWithClickedButtonIndex(0, true);
                    alert = null;
                });
                NSRunLoop.Main.AddTimer(tmr, NSRunLoopMode.Common);
            });
        }
    }
}
