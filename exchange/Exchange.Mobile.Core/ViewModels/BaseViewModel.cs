using MvvmCross.ViewModels;

namespace Exchange.Mobile.Core.ViewModels

{
    public class BaseViewModel : MvxViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}
