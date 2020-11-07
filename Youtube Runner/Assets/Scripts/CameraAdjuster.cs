using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public float cameraWidth = 16;
    public float cameraHeight = 9;

    private void Start()
    {
        AdjustCameraSize();
    }

    public void AdjustCameraSize()
    {
        // set the desired aspect ratio
        float targetAspect = cameraWidth / cameraHeight;

        // determine the game window's current aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;//the casts are needed.

        // current viewport height should be scaled by this amount
        float scaleHeight = windowAspect / targetAspect;

        // obtain camera component so we can modify its viewport
        Camera camera = Camera.main;

        // if scaled height is less than current height, add letterbox
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}