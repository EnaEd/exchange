using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Exchange.Mobile.Core.ViewModels
{
    public class OfferViewModel : MvxViewModel
    {
        public OfferViewModel()
        {

        }

        #region Commands
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        #endregion Commands

        #region Functionality
        private void ResetText()
        {
            Text = "Hello MvvmCross";

        }
        #endregion Functionality

        #region Properties
        private string _text = "Hello MvvmCross";
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        #endregion Properties
    }
}
