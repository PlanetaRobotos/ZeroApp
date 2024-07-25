using UnityEngine;

namespace _Project.Scripts.Environment
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GameFieldScaler: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetScale()
        {
            _spriteRenderer.size = new Vector2(1, 1);
        }
    }
}