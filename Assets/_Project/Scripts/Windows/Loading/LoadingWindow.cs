using _Project.Scripts.Infrastructure.Extensions;
using _Project.Scripts.Windows.Loading.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Windows.Loading
{
    public class LoadingWindow : BaseWindow
    {
        [SerializeField] private Button _screenButton;
        [SerializeField] private Transform _loadingViewParent;

        [Inject] private ILoadingScreenProvider LoadingScreenProvider { get; }
        [Inject] private WindowsController WindowsController { get; }
        
        private int _clicksCounter;

        public override void OnOpen()
        {
            // CreateLoadingView();

            CloseWithDelay().Forget();
        }
        
        private async UniTaskVoid CloseWithDelay()
        {
            // await UniTask.Delay(1);
            Close();
        }

        public override void Close()
        {
            base.Close();
        }

        private void CreateLoadingView()
        {
            var screen = LoadingScreenProvider.GetScreen(_loadingViewParent).transform;
            screen.transform.name = "LoadingBackground";
            screen.SetParentInZeroPosition(_loadingViewParent);
        }
    }
}