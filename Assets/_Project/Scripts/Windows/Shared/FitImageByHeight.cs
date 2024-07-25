using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.Shared
{
    [RequireComponent(typeof(Image))]
    public class FitImageByHeight : MonoBehaviour
    {
        void Start()
        {
            var myImage = GetComponent<Image>();
            var myRectTransform = GetComponent<RectTransform>();

            // Assuming the canvas is using Screen Space - Overlay
            // If using Screen Space - Camera, you might need to adjust this calculation
            float screenAspect = (float)Screen.height / (float)Screen.width;
            float imageAspect = myImage.sprite.bounds.size.y / myImage.sprite.bounds.size.x;

            if (screenAspect > imageAspect)
            {
                // Screen is taller than the image's aspect ratio
                float scaleFactor = screenAspect / imageAspect;
                myRectTransform.sizeDelta = new Vector2(myRectTransform.sizeDelta.x * scaleFactor, myRectTransform.sizeDelta.y);
            }
            else
            {
                // Screen is wider than the image's aspect ratio (or equal)
                // Adjust width to maintain image aspect ratio based on screen height
                float width = Screen.height / imageAspect;
                myRectTransform.sizeDelta = new Vector2(width, Screen.height);
            }
        }
    }
}