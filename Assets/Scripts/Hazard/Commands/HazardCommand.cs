using UnityEngine;


public abstract class HazardCommand : ScriptableObject {}

namespace ProjectileCommands {

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
}

namespace LaserCommands {

    public class MoveBy : HazardCommand{
        
        public static float GetSignedAngle(Vector2 p1, Vector2 p2)
        {
            return Mathf.Atan2(p1.y*p2.x - p1.x*p2.y, p1.x*p2.x+p1.y*p2.y) * Mathf.Rad2Deg;
        }
        private Transform transform;
        private Vector2 point;
        private Vector2 origin;
        private float angle;
        public float Angle {get {
            if(this.transform)
                this.angle = MoveBy.GetSignedAngle(Vector2.up, (Vector2) transform.position - this.origin);   
            return this.angle;
        }}
        private float speed;
        public float Speed => speed;
        public MoveBy(float angle, float speed)
        {
            this.angle = angle;
            this.speed = speed;
        }
        public MoveBy(Vector2 origin, Transform transform, float speed)
        {
            this.transform = transform;
            this.origin = origin;
            this.speed = speed;
        }
        public MoveBy(Vector3 origin, Vector3 point, float speed){
            this.point = point;
            this.origin = origin;
            this.angle = Vector2.SignedAngle(Vector2.up, point - origin);
            this.speed = speed;
        }
        public MoveBy(Vector2 origin, Vector2 point, float speed){
            this.point = point;
            this.origin = origin;
            this.angle = Vector2.SignedAngle(Vector2.up, point - origin);
            this.speed = speed;
        }
    }


    public class ToggleActivation : HazardCommand
    {
        private bool activate;
        public bool Activate => activate;
        private float startAngle;
        public float StartAngle => startAngle;

        public ToggleActivation(bool activate, float startAngle=0)
        {
            this.activate = activate;
            this.startAngle = startAngle;
        }
    }

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

}
