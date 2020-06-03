using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.ViewModels
{
    public class MainTabbedViewModel : BaseViewModel
    {
        private bool _isFirstTime = true;
        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                NavigationService.Navigate<UploadOfferViewModel>(),
                NavigationService.Navigate<OfferViewModel>(),
                NavigationService.Navigate<OfferDiscussViewModel>(),
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_isFirstTime)
            {
                ShowInitialViewModels();
                _isFirstTime = false;
            }
        }
    }
}
