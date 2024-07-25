using UnityEngine;

namespace _Project.Scripts.Infrastructure.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class SimpleObserver : MonoBehaviour
    {
        private ITriggable _parent;
        private Collider2D _collider;

        public ITriggable Parent => _parent;
        public Collider2D Collider => _collider;

        public void Setup(ITriggable parent)
        {
            _parent = parent;
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other) => _parent?.TriggerEnter(other);

        private void OnTriggerExit2D(Collider2D other) => _parent?.TriggerExit(other);
        public void SetActive(bool active) => gameObject.SetActive(active);
    }

    public interface ITriggable
    {
        void TriggerEnter(Collider2D other);
        void TriggerExit(Collider2D other);
    }
}