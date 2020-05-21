using Exchange.Mobile.Core.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPopupView : PopupPage
    {
        private OfferViewModel _viewModel;
        public CategoryPopupView()
        {
            InitializeComponent();
        }

        public CategoryPopupView(OfferViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        private async void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("you selected", $" select {e.SelectedItem}", "Ok");
            await _viewModel.ShowOfferAsync(e.SelectedItem as object);
            await PopupNavigation.Instance.PopAsync();

        }
    }
}
