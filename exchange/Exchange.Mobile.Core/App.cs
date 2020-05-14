using Exchange.Mobile.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Exchange.Mobile.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<HomeViewModel>();
        }
    }
}
