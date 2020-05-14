using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.ViewModels;

namespace Exchange.Mobile.UI.Droid
{
    public class Setup : MvxAndroidSetup<Core.App>
    {
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            return new Core.App();
        }
    }
}
