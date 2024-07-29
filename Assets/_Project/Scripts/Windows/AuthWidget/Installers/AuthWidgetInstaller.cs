using _Project.UI.Mediators;
using _Project.Windows.AuthWidget.Mediators;
using _Project.Windows.AuthWidget.Views;
using ServiceLocator.Core;

namespace _Project.Windows.AuthWidget.Installers
{
    public class AuthWidgetInstaller : BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<BaseUIMediator<AuthWindow>>(new AuthWidgetMediator());
        }
    }
}