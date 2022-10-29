using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class HazardCommand {}

// SLOW ARRIVAL NOT WORKING - see hazardprojectile
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
    private bool slowArrival;
    public bool SlowArrival => slowArrival;

    private float slowArrivalRadius;
    public float SlowArrivalRadius => slowArrivalRadius;
    private bool stopAtDestination;
    public bool StopAtDestination => stopAtDestination;
    private float timeLimit;
    public float TimeLimit => timeLimit;
    private float startTime;
    public float StartTime => startTime;


    public MoveCommand(Vector2 from, Vector2 to, float speed, bool stopAtDestination=true, bool slowArrival=false, float slowArrivalRadius=0, float timeLimit=30)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
        this.slowArrival = slowArrival;
        this.slowArrivalRadius = slowArrivalRadius;
        this.stopAtDestination = stopAtDestination;
        this.timeLimit = timeLimit;
    }

    public MoveCommand(Vector2 from, Transform to, float speed, bool stopAtDestination=true, bool slowArrival=false, float slowArrivalRadius=0, float timeLimit=30)
    {
        this.from = from;
        this.toTransform = to;
        this.speed = speed;
        this.slowArrival = slowArrival;
        this.slowArrivalRadius = slowArrivalRadius;
        this.stopAtDestination = stopAtDestination;
        this.timeLimit = timeLimit;
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