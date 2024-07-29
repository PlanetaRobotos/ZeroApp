using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.Extensions;
using _Project.Windows.Loading.Providers;
using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Windows.Loading.Installers
{
    public class LoadingScreenInstaller : BaseMonoServicesRegistrator
    {
        [SerializeField] private string _loadingScreenViewResourcePath;
        [SerializeField] private Transform _parent;

        [Inject] private UIMainController UIMainController { get; }

        public override void Register()
        {
            LoadingScreenFromResourcesProvider provider =
                new LoadingScreenFromResourcesProvider(_loadingScreenViewResourcePath);
            Locator.Register<ILoadingScreenProvider>(provider);

            // CreateLoadingScreen(provider);
        }

        private void CreateLoadingScreen(ILoadingScreenProvider provider)
        {
            provider.GetScreen(_parent).transform.SetParentInZeroPosition(_parent);
        }
    }
}