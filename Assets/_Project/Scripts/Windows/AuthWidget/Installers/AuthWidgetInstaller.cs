using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.AuthWidget;
using _Project.Scripts.Windows.BoardWidget.Handlers;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Windows.BoardWidget.Installers
{
    public class AuthWidgetInstaller: BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<BaseUIMediator<AuthWindow>>(new AuthWidgetMediator());
        }
    }
}