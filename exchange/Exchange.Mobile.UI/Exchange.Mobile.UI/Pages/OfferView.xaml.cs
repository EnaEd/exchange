using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.ViewModels;
using Exchange.Mobile.UI.Pages.Popups;
using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class OfferView : MvxContentPage<OfferViewModel>
    {
        public OfferView()
        {
            InitializeComponent();
        }

        private async void SwipeCardView_Swiped(object sender, MLToolkit.Forms.SwipeCardView.Core.SwipedCardEventArgs args)
        {
            if (await ViewModel.CheckIsLastOffer(args.Item as OfferCardModel))
            {
                await ViewModel.ShowOfferAsync(ViewModel.CurrentCategory);
            }


            if (args.Direction == SwipeCardDirection.Right)
            {
                //TODO EE:set offer for change
                ViewModel.CurrentOfferCard = args.Item as OfferCardModel;
                await PopupNavigation.Instance.PushAsync(new SetOfferPopupView(ViewModel));
            }
            if (args.Direction == SwipeCardDirection.Up)
            {
                //TODO EE:Add to favorite
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.CurrentCategory is null)
            {
                await PopupNavigation.Instance.PushAsync(new CategoryPopupView(ViewModel));
            }
        }

        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }

        private void SwipeCardView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName.Equals(Constant.Shared.TOP_ITEM))
            {
                ViewModel.CurrentOfferCard = (sender as SwipeCardView).TopItem as OfferCardModel;
            }
        }
    }
}
