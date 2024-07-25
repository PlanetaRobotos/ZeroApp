using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure
{
    [ExecuteAlways]
    [ExecuteInEditMode]
    public class UIScaler : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private RectTransform _uiCanvasRect;

        private Vector2 _currentUICanvasSize;

        private void Awake()
        {
            SetScale();
        }

        private void SetScale()
        {
            _canvasScaler.matchWidthOrHeight = _uiCamera.aspect < 1.6f
                ? _uiCamera.aspect < 1.4f ? 0.2f : 0.4f
                : 1f;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (_currentUICanvasSize != _uiCanvasRect.sizeDelta)
            {
                _currentUICanvasSize = _uiCanvasRect.sizeDelta;
                SetScale();
            }
        }
#endif
    }
}