using UnityEngine;

namespace _Project.Scripts.Infrastructure.Extensions
{
    public static class TransformExtensions
    {
        public static void SetParentInZeroPosition(this Transform transform, Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
}