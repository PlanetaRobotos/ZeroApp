using UnityEngine;
using UnityEngine.UI;

namespace _Project.Windows.Shared
{
    [RequireComponent(typeof(Image))]
    public class FitImageByHeight : MonoBehaviour
    {
        private void Start()
        {
            Image myImage = GetComponent<Image>();
            RectTransform myRectTransform = GetComponent<RectTransform>();

            // Assuming the canvas is using Screen Space - Overlay
            // If using Screen Space - Camera, you might need to adjust this calculation
            float screenAspect = Screen.height / (float)Screen.width;
            float imageAspect = myImage.sprite.bounds.size.y / myImage.sprite.bounds.size.x;

            if (screenAspect > imageAspect)
            {
                // Screen is taller than the image's aspect ratio
                float scaleFactor = screenAspect / imageAspect;
                myRectTransform.sizeDelta =
                    new Vector2(myRectTransform.sizeDelta.x * scaleFactor, myRectTransform.sizeDelta.y);
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