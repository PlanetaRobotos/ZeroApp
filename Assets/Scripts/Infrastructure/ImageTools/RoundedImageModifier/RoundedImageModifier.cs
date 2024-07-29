using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure.ImageTools.RoundedImageModifier
{
    [RequireComponent(typeof(Image))]
    public class RoundedImageModifier : BaseMeshEffect
    {
        public Vector2 offset;
        [Range(0f, 1f)] public float radius = 0.1f;
        [Range(0, 10)] public int points = 2;

        private Vector2 Size => graphic.rectTransform.rect.size;
        private float _halfMask;
        private UIVertex _vertex;
        private UIVertex _bl;
        private UIVertex _tr;
        private Vector4 _uv0Size;
        private float MinHalf => Mathf.Min(Size.x, Size.y) / 2f;

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            vh.PopulateUIVertex(ref _bl, 0);
            vh.PopulateUIVertex(ref _tr, 2);

            _uv0Size = _tr.uv0 - _bl.uv0;
            vh.Clear();

            _vertex.color = graphic.color;
            _vertex.position.x = 0;
            _vertex.position.y = 0;
            _vertex.uv0 = _bl.uv0 + (_uv0Size * 0.5f);

            vh.AddVert(_vertex);

            float rad = (2 * Mathf.PI) / ((points + 1) * 4);

            for (int i = 0; i < ((points + 1) * 4); i++)
            {
                float cos = Mathf.Cos(i * rad);
                float sin = Mathf.Sin(i * rad);
                float x = (Mathf.Sign(cos) * (1 - radius) + cos * radius) * MinHalf;
                float y = (Mathf.Sign(sin) * (1 - radius) + sin * radius) * MinHalf;
                if (i % (points + 1) == 0)
                {
                    if (Mathf.Abs(cos) < 0.001f && Mathf.Abs(sin) == 1)
                    {
                        SetVertexPosAndTexture(-x, y);
                    }

                    if (Mathf.Abs(sin) < 0.001f && Mathf.Abs(cos) == 1)
                    {
                        SetVertexPosAndTexture(x, -y);
                    }

                    vh.AddVert(_vertex);
                }

                SetVertexPosAndTexture(x, y);
                vh.AddVert(_vertex);
            }

            for (int i = 0; i < ((points + 2) * 4) - 1; i++)
            {
                vh.AddTriangle(0, i + 2, i + 1);
            }

            vh.AddTriangle(0, 1, ((points + 2) * 4));
        }

        private void SetVertexPosAndTexture(float maskPosX, float maskPosY)
        {
            float signX = Mathf.Sign(maskPosX);
            float signY = Mathf.Sign(maskPosY);
            maskPosX += Mathf.Clamp((Size.x - Size.y) / 2f, 0, Single.MaxValue) * signX + offset.x * signX;
            maskPosY += Mathf.Clamp((Size.y - Size.x) / 2f, 0, Single.MaxValue) * signY + offset.y * signY;
            _vertex.position.x = maskPosX;
            _vertex.position.y = maskPosY;
            _vertex.uv0.x = _bl.uv0.x + _uv0Size.x * (maskPosX + Size.x / 2f) / Size.x;
            _vertex.uv0.y = _bl.uv0.y + _uv0Size.y * (maskPosY + Size.y / 2f) / Size.y;
        }
    }
}