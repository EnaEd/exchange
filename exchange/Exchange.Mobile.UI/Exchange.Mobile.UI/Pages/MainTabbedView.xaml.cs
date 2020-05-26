using Exchange.Mobile.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    public partial class MainTabbedView : MvxTabbedPage<MainTabbedViewModel>
    {
        public MainTabbedView()
        {
            InitializeComponent();
        }
    }
}
