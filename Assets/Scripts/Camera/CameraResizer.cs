using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer cameraBounds;

    private void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float boundsRatio = cameraBounds.bounds.size.x / cameraBounds.bounds.size.y;

        if (screenRatio >= boundsRatio)
        {
            Camera.main.orthographicSize = cameraBounds.bounds.size.y / 2;
        }
        else
        {
            float difference = boundsRatio / screenRatio;
            Camera.main.orthographicSize = cameraBounds.bounds.size.y / 2 * difference;
        }
    }
}
