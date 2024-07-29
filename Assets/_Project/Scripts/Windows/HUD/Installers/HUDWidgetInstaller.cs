using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;

namespace _Project.Scripts.Windows.BoardWidget.Installers
{
    public class HUDWidgetInstaller: BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<BaseUIMediator<HUDWindow>>(new HudWidgetMediator());
        }
    }
}