using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MoveTest")]
public class MoveCommand : HazardCommand 
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
    private bool slowArrival;
    public bool SlowArrival => slowArrival;

    private float slowArrivalRadius;
    public float SlowArrivalRadius => slowArrivalRadius;

    [SerializeField]
    private float timeLimit;
    public float TimeLimit => timeLimit;


    public MoveCommand(Vector2 from, Vector2 to, float speed, bool slowArrival=false, float slowArrivalRadius=0, float timeLimit=30)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
        this.slowArrival = slowArrival;
        this.slowArrivalRadius = slowArrivalRadius;
        this.timeLimit = timeLimit;
    }

    public MoveCommand(Vector2 from, Transform to, float speed, bool slowArrival=false, float slowArrivalRadius=0, float timeLimit=30)
    {
        this.from = from;
        this.toTransform = to;
        this.speed = speed;
        this.slowArrival = slowArrival;
        this.slowArrivalRadius = slowArrivalRadius;
        this.timeLimit = timeLimit;
    }

}
