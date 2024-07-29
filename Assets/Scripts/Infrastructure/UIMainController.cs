using _Project.Windows.Particles;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Infrastructure
{
    public class UIMainController : MonoBehaviour
    {
        [SerializeField] private RectTransform mainUIWindowRoot;
        [SerializeField] private AssetReference mainUIWindowRef;
        [SerializeField] private Canvas _mainUICanvas;
        [SerializeField] private Camera _mainUICamera;
        [SerializeField] private WindowsController _windowsController;
        [SerializeField] private RectTransform _overlayRoot;
        [SerializeField] private MainUIParticlesObserver _particlesObserver;
        [SerializeField] private Camera _mainCamera;

        public RectTransform mainUIWindowRootRect => mainUIWindowRoot;
        public RectTransform OverlayRoot => _overlayRoot;
        public Canvas UICanvas => _mainUICanvas;
        public Camera UICamera => _mainUICamera;
        public Camera MainCamera => _mainCamera;
        public MainUIParticlesObserver ParticlesObserver => _particlesObserver;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnBackButtonClick();
        }

        private void OnBackButtonClick()
        {
            var spawnedWindows = _windowsController.SpawnedWindows;

            if (spawnedWindows.Count > 0)
                spawnedWindows[^1].OnBackButtonClick();
        }
    }
}