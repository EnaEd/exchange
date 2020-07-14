using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Exchange.Mobile.Core.Helpers.Interface;
using Plugin.CurrentActivity;
using System;
using System.Threading.Tasks;

namespace Exchange.Mobile.UI.Droid.Services
{
    public class NativeLocationService : INativeLocationService
    {
        public async Task TurnOnGPS()
        {
            try
            {
                MainActivity activity = CrossCurrentActivity.Current.Activity as MainActivity;

                GoogleApiClient googleApiClient = new GoogleApiClient.Builder(activity)
                    .AddApi(LocationServices.API).Build();
                googleApiClient.Connect();
                LocationRequest locationRequest = LocationRequest.Create();
                locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
                locationRequest.SetInterval(10000);
                locationRequest.SetFastestInterval(10000 / 2);

                LocationSettingsRequest.Builder
                        locationSettingsRequestBuilder = new LocationSettingsRequest.Builder()
                        .AddLocationRequest(locationRequest);
                locationSettingsRequestBuilder.SetAlwaysShow(false);
                LocationSettingsResult locationSettingsResult = await LocationServices.SettingsApi.CheckLocationSettingsAsync(
                    googleApiClient, locationSettingsRequestBuilder.Build());

                if (locationSettingsResult.Status.StatusCode == LocationSettingsStatusCodes.ResolutionRequired)
                {
                    locationSettingsResult.Status.StartResolutionForResult(activity, 0);
                }
            }
            catch (Exception ex)
            {
                //TODO handle this exception
                throw new NotImplementedException(ex.Message);
            }

        }
    }
}
