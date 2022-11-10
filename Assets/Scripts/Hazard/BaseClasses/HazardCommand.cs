using UnityEngine;

// [System.Serializable]
public abstract class HazardCommand {}//: ScriptableObject {}

// [System.Serializable]
// [CreateAssetMenu(menuName="Hazards/Commands/Move")]
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

// [System.Serializable]
// [CreateAssetMenu(menuName="Hazards/Commands/Wait")]
public class WaitCommand : HazardCommand
{
    [SerializeField]
    private float seconds;
    public float Seconds => seconds;

    public WaitCommand(float seconds)
    {
        this.seconds = seconds;
    }

}
// [System.Serializable]
// [CreateAssetMenu(menuName="Hazards/Commands/Fire")]
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
// [System.Serializable]
// [CreateAssetMenu(menuName="Hazards/Commands/Change Drag")]
public class DragChangeCommand : HazardCommand
{
    [SerializeField]
    private float linearDrag;
    public float LinearDrag => linearDrag;
    [SerializeField]
    private float angularDrag;
    public float AngularDrag => angularDrag;

    public DragChangeCommand(float linearDrag, float angularDrag=0)
    {
        this.linearDrag = linearDrag;
        this.angularDrag = angularDrag;
    }

}
// [System.Serializable]
// [CreateAssetMenu(menuName="Hazards/Commands/Change Gravity")]
public class GravityChangeCommand : HazardCommand
{
    [SerializeField]
    private float gravity;
    public float Gravity => gravity;

    public GravityChangeCommand(float gravity)
    {
        this.gravity = gravity;
    }
}
