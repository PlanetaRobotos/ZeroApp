using _Project.UI.Mediators;
using _Project.Windows.HUD.Mediators;
using _Project.Windows.HUD.Views;
using ServiceLocator.Core;

namespace _Project.Windows.HUD.Installers
{
    public class HUDWidgetInstaller : BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<BaseUIMediator<HUDWindow>>(new HudWidgetMediator());
        }
    }
}