using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Windows.BoardWidget.Installers
{
    public class SelectModeWidgetInstaller: BaseMonoServicesRegistrator
    {
        [SerializeField] private AssetReference _networkPlayerRef;
        
        public override void Register()
        {
            Locator.Register<BaseUIMediator<SelectModeWindow>>(new SelectModeWindowMediator(_networkPlayerRef));
        }
    }
}