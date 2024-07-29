using UnityEngine;

namespace _Project.Windows.Shared
{
    [RequireComponent(typeof(RectTransform))]
    public class ClampContentSize : MonoBehaviour
    {
        [SerializeField] private RectTransform _targetRT;
        private float _maxHeight;
        private float _minHeight;

        private RectTransform _rt;

        private RectTransform TargetRT =>
            _targetRT ? _targetRT : _rt;

        private void FixedUpdate()
        {
            HandleSize();
        }

        public void Init(float minHeight, float maxHeight)
        {
            _rt = GetComponent<RectTransform>();

            _minHeight = minHeight;
            _maxHeight = maxHeight;
        }

        public void HandleSize()
        {
            float currentHeight = TargetRT.sizeDelta.y;
            float clampedHeight = Mathf.Clamp(currentHeight, _minHeight, _maxHeight);
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, clampedHeight);
        }
    }
}