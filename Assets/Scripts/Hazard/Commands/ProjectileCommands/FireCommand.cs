using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCommand : HazardCommand
{
    [SerializeField]
    private Vector2 from;
    public Vector2 From => from;
    private Transform toTransform;
    public Transform ToTransform => toTransform;

    [SerializeField]
    private Vector2 to;
    public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position :  to ;} }

    [SerializeField]
    private float speed;
    public float Speed => speed;

    public FireCommand(Vector2 from, Transform to, float speed)
    {
        this.from = from;
        this.toTransform = to;
        this.speed = speed;
    }
    public FireCommand(Vector2 from, Vector2 to, float speed)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
    }

}
