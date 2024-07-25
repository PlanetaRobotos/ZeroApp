using _Project.Scripts.Windows.Loading.Views;
using UnityEngine;

namespace _Project.Scripts.Windows.Loading.Providers
{
    public class LoadingScreenFromResourcesProvider : ILoadingScreenProvider
    {
        private readonly string _resourcePath;
        private LoadingScreenView _currentScreenView;

        public LoadingScreenFromResourcesProvider(string resourcePath)
        {
            _resourcePath = resourcePath;
        }

        public LoadingScreenView GetScreen(Transform parent)
        {
            if (_currentScreenView == null)
                return _currentScreenView = Object
                    .Instantiate(Resources.Load<GameObject>(_resourcePath))
                    .GetComponent<LoadingScreenView>();
            return _currentScreenView;
        }
    }
    
    public class LoadingScreenProvider : ILoadingScreenProvider
    {
        private readonly LoadingScreenView _template;
        private LoadingScreenView _currentScreenView;

        public LoadingScreenProvider(LoadingScreenView template)
        {
            _template = template;
        }

        public LoadingScreenView GetScreen(Transform parent)
        {
            if (_currentScreenView == null)
                return _currentScreenView = Object.Instantiate(_template);
            return _currentScreenView;
        }
    }
}