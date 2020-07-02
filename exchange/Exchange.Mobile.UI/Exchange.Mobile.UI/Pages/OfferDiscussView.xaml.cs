using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class OfferDiscussView : MvxContentPage<OfferDiscussViewModel>
    {
        public OfferDiscussView()
        {
            InitializeComponent();
            BindingContext = this.BindingContext;
        }
        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }


        private void SwipeItem_Invoked(object sender, System.EventArgs e)
        {
            ViewModel.DeleteDiscussCommandAsync.Execute((sender as SwipeItemView).CommandParameter);
        }
    }
}
