using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class HazardCommand {}

public class MoveCommand : HazardCommand 
{
    private Vector2 from;
    public Vector2 From => from;
    private Transform toTransform;
    public Transform ToTransform => toTransform;

    private Vector2 to;
    public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position :  to ;} }
    private float speed;
    public float Speed => speed;

    public MoveCommand(Vector2 from, Vector2 to, float speed)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
    }

    public MoveCommand(Vector2 from, Transform to, float speed)
    {
        this.from = from;
        this.toTransform = to;
        this.speed = speed;
    }

}

public class WaitCommand : HazardCommand
{
    private float seconds;
    public float Seconds => seconds;

    public WaitCommand(float seconds)
    {
        this.seconds = seconds;
    }

}

public class FireCommand : HazardCommand
{
    private Vector2 from;
    public Vector2 From => from;
    private Transform toTransform;
    public Transform ToTransform => toTransform;

    private Vector2 to;
    public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position :  to ;} }

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

public class DragChangeCommand : HazardCommand
{
    private float linearDrag;
    public float LinearDrag => linearDrag;
    private float angularDrag;
    public float AngularDrag => angularDrag;

    public DragChangeCommand(float linearDrag, float angularDrag=0)
    {
        this.linearDrag = linearDrag;
        this.angularDrag = angularDrag;
    }

}