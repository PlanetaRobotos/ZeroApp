using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure
{
    public class EmptyGraphic : Graphic, ICanvasRaycastFilter
    {
        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera) => enabled &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out _);

        protected override void OnPopulateMesh(VertexHelper vh) => vh.Clear();

        private void OnDrawGizmos()
        {
            if (!enabled) return;
        
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            var _color = Gizmos.color;
            Gizmos.color = color;
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    var c1 = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[i]);
                    var c2 = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[j]);
                    Gizmos.DrawLine(c1, c2);
                }
            }

            Gizmos.color = _color;
        }
    }
}