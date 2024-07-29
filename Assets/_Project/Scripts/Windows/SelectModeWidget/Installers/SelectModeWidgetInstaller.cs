using _Project.UI.Mediators;
using _Project.Windows.SelectModeWidget.Mediators;
using _Project.Windows.SelectModeWidget.Views;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Windows.SelectModeWidget.Installers
{
    public class SelectModeWidgetInstaller : BaseMonoServicesRegistrator
    {
        [SerializeField] private AssetReference _networkPlayerRef;

        public override void Register()
        {
            Locator.Register<BaseUIMediator<SelectModeWindow>>(new SelectModeWindowMediator(_networkPlayerRef));
        }
    }
}