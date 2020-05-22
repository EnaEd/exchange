using Exchange.Mobile.Core.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetOfferPopupView : PopupPage
    {
        public OfferViewModel ViewModel { get; set; }
        public SetOfferPopupView()
        {
            InitializeComponent();
        }
        public SetOfferPopupView(OfferViewModel viewModel, object item)
        {
            InitializeComponent();
            ViewModel = viewModel;
            BindingContext = ViewModel;
        }
    }
}
