using Exchange.Mobile.Core.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exchange.Mobile.UI.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetOfferPopupView : PopupPage
    {
        private Forms9Patch.BubblePopup _bubble;
        public OfferViewModel ViewModel { get; set; }
        public SetOfferPopupView()
        {
            InitializeComponent();
        }
        public SetOfferPopupView(OfferViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            BindingContext = ViewModel;
        }

        private void KebabMenuTapped(object sender, System.EventArgs e)
        {

            SetUpToolTip();

            _bubble.Target = sender as VisualElement;
            _bubble.IsVisible = true;

            _bubble.PointerDirection = Forms9Patch.PointerDirection.Down;

        }

        private void SetUpToolTip()
        {
            var infoGesture = new TapGestureRecognizer();
            infoGesture.Tapped += (sender, args) =>
            {

                _bubble.CancelAsync();
            };

            var addDescriptionGesture = new TapGestureRecognizer();
            addDescriptionGesture.Tapped += (sender, args) =>
            {
                _bubble.CancelAsync();
            };

            var infoLabel = new Label
            {
                Text = "\uf059",
                FontFamily = (OnPlatform<string>)Application.Current.Resources["FontAwesomeSolid"],
                FontSize = 30
            };
            infoLabel.GestureRecognizers.Add(infoGesture);

            var addDescriptionLabel = new Label
            {
                Text = "\uf15c",
                FontFamily = (OnPlatform<string>)Application.Current.Resources["FontAwesomeSolid"],
                FontSize = 30
            };
            addDescriptionLabel.GestureRecognizers.Add(addDescriptionGesture);

            _bubble = new Forms9Patch.BubblePopup(this)
            {

                PointerCornerRadius = default(int),
                PointerLength = 5,
                BorderRadius = 4,
                PageOverlayColor = Color.Transparent,
                BackgroundColor = Color.White,

                Content = new StackLayout
                {
                    Children =
                    {
                        infoLabel,
                        addDescriptionLabel
                    }
                }
            };
        }

        private void HelpMenuTapped(object sender, System.EventArgs e)
        {

        }

    }
}
