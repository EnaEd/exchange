using Exchange.Mobile.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core
{
    public class App : MvxApplication
    {
        public override Task Startup()
        {
            return base.Startup();
        }
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
               .EndingWith("Helper")
               .AsInterfaces()
               .RegisterAsLazySingleton();
            RegisterAppStart<AuthViewModel>();
        }
    }
}
