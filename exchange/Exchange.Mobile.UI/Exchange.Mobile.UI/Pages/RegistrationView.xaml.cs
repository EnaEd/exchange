using Exchange.Mobile.Core.Models.GooglesModels;
using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationView : MvxContentPage<RegistrationViewModel>
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as GooglePlaceAutoCompletePrediction;
            ViewModel.CurrentSearchLocation = ViewModel.Places.FirstOrDefault(place => place.StructuredFormatting.MainText.Equals(item.StructuredFormatting.MainText));
            ViewModel.IsAutoCompleteVisible = false;
        }
    }
}
