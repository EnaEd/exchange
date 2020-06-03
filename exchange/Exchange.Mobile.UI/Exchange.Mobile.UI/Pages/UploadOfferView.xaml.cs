using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
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

    }
}
