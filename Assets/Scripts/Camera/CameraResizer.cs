using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer cameraBounds;

    private void Awake()
    {
        cameraBounds.color = new Color(0,0, 0, 0);
    }

    private void Start()
    {

        Camera.main.orthographicSize = cameraBounds.bounds.size.x * Screen.height / Screen.width * 0.5f;

        // Fancier alternative the video had for making sure the bounds are set regardless of orientation or aspect ratio
        //    This feel not needed to me but leaving here just in case
        //float screenRatio = (float)Screen.width / (float)Screen.height;
        //float boundsRatio = cameraBounds.bounds.size.x / cameraBounds.bounds.size.y;

        /*
        if (screenRatio >= boundsRatio)
        {
            Camera.main.orthographicSize = cameraBounds.bounds.size.y / 2;
        }
        else
        {
            float difference = boundsRatio / screenRatio;
            Camera.main.orthographicSize = cameraBounds.bounds.size.y / 2 * difference;
        }
        */
    }
}
