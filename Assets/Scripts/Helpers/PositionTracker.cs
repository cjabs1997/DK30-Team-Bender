using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private Transform transformToTrack;
    public Transform TransformToTrack => transformToTrack;
    [SerializeField]
    private Vector2 offset;
    public Vector2 Offset => offset;
    #endregion
    
    void FixedUpdate()
    {
        this.transform.position = this.transformToTrack.position + (Vector3) offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
