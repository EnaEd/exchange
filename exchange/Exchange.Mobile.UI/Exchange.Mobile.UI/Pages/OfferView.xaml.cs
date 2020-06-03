using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.ViewModels;
using Exchange.Mobile.UI.Pages.Popups;
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

        private async void SwipeCardView_Swiped(object sender, MLToolkit.Forms.SwipeCardView.Core.SwipedCardEventArgs e)
        {
            if (e.Direction == SwipeCardDirection.Right)
            {
                //TODO EE:set offer for change
                ViewModel.CurrentOfferCard = e.Item as OfferCardModel;
                await PopupNavigation.Instance.PushAsync(new SetOfferPopupView(ViewModel));
            }
            if (e.Direction == SwipeCardDirection.Up)
            {
                //TODO EE:Add to favorite
            }
        }

        private async void SwipeCardView_Dragging(object sender, MLToolkit.Forms.SwipeCardView.Core.DraggingCardEventArgs e)
        {
            await ViewModel.ShowOfferAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await PopupNavigation.Instance.PushAsync(new CategoryPopupView(ViewModel));
        }

        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }

    }
}
