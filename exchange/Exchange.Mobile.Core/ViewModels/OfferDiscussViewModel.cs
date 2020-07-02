using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferDiscussViewModel : BaseViewModel
    {

        #region variables
        private readonly IDiscussOfferService _discussOfferService;
        private readonly IAuthService<User> _authService;
        #endregion variables


        public OfferDiscussViewModel(IDiscussOfferService discussOfferService, IAuthService<User> authService)
        {
            _discussOfferService = discussOfferService;
            _authService = authService;

        }

        #region properties
        public ObservableCollection<DiscussOfferModel> Discusses { get; set; } = new ObservableCollection<DiscussOfferModel>();
        #endregion properties

        #region overrides

        public override void ViewAppearing()
        {
            Device.InvokeOnMainThreadAsync(async () =>
            {
                await ShowDiscuss();
            });
            base.ViewAppearing();
        }


        #endregion overrides

        #region commands
        public IMvxCommand DeleteDiscussCommandAsync => new MvxAsyncCommand<object>(DeleteDiscussAsync);


        #endregion commands

        #region functionality
        private async Task DeleteDiscussAsync(object args)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    var result = await _discussOfferService.DeleteDiscussOfferAsync(args as DiscussOfferModel);
                    IsBusy = false;
                    await ShowDiscuss();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task ShowDiscuss()
        {
            try
            {

                if (!IsBusy)
                {
                    IsBusy = true;
                    User user = await _authService.GetUserByPhoneAsync(new PhoneRequestModel { PhoneNumber = PhoneNumber });

                    Discusses = new ObservableCollection<DiscussOfferModel>(
                        await _discussOfferService.GetUserDiscussAsync(new DiscussOfferRequestModel { UserId = user.Id }));

                    await RaisePropertyChanged(nameof(Discusses));


                }


            }
            catch (System.Exception ex)
            {
                var e = ex.Message;
                DisplayAlertService.ShowToast(Constant.Shared.FAIL_TO_LOAD_DISCUSS);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion functionality
    }
}
