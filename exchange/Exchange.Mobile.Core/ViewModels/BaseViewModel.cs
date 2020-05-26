using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Exchange.Mobile.Core.ViewModels

{
    public class BaseViewModel : MvxViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        public BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}
