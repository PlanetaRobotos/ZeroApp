using UnityEngine;

namespace _Project.Scripts.Windows.Shared
{
    [RequireComponent(typeof(RectTransform))]
    public class ClampContentSize : MonoBehaviour
    {
        [SerializeField] private RectTransform _targetRT;

        private RectTransform _rt;
        private float _minHeight;
        private float _maxHeight;

        private RectTransform TargetRT =>
            _targetRT ? _targetRT : _rt;

        public void Init(float minHeight, float maxHeight)
        {
            _rt = GetComponent<RectTransform>();
            
            _minHeight = minHeight;
            _maxHeight = maxHeight;
        }

        private void FixedUpdate()
        {
            HandleSize();
        }

        public void HandleSize()
        {
            float currentHeight = TargetRT.sizeDelta.y;
            float clampedHeight = Mathf.Clamp(currentHeight, _minHeight, _maxHeight);
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, clampedHeight);
        }
    }
}