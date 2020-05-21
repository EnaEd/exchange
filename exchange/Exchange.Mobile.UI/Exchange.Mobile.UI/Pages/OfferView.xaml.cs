using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferView : MvxContentPage<OfferViewModel>
    {
        public OfferView()
        {
            InitializeComponent();
        }

        private async void SwipeCardView_Swiped(object sender, MLToolkit.Forms.SwipeCardView.Core.SwipedCardEventArgs e)
        {
            await DisplayAlert("swipe event", e.Direction.ToString(), "OK");
        }

        private async void SwipeCardView_Dragging(object sender, MLToolkit.Forms.SwipeCardView.Core.DraggingCardEventArgs e)
        {
            await ViewModel.ShowOfferAsync();
        }
    }
}
