using Android.App;
using Exchange.Mobile.UI.Droid.Helpers;
using Firebase.Messaging;

namespace Exchange.Mobile.UI.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FireBaseMessageService : FirebaseMessagingService
    {
        public FireBaseMessageService()
        {
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            new NotificationHelper().CreateNotification(message.GetNotification().Title, message.GetNotification().Body);
        }
    }
}
