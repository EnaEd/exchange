using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class UploadOfferView : MvxContentPage<UploadOfferViewModel>
    {
        public UploadOfferView()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as GooglePlaceAutoCompletePrediction;
            ViewModel.CurrentSearchLocation = ViewModel.Places.FirstOrDefault(place => place.StructuredFormatting.MainText.Equals(item.StructuredFormatting.MainText));
            ViewModel.IsAutoCompleteVisible = false;
        }
    }
}
