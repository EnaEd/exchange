using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Linq;
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
            BindingContext = _viewModel;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.SearchLocation))
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as GooglePlaceAutoCompletePrediction;
            _viewModel.CurrentSearchLocation = _viewModel.Places.FirstOrDefault(place => place.StructuredFormatting.MainText.Equals(item.StructuredFormatting.MainText));
            _viewModel.IsAutoCompleteVisible = false;
        }
    }
}
