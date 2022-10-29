using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardProjectile : MonoBehaviour
{
    [SerializeField] bool rotate;
    [SerializeField] float degreesPerSecond;
    [SerializeField] float maxSteeringForce;

    Rigidbody2D m_RigidBody; 
    SpriteRenderer spriteRenderer;

    private Queue<HazardCommand> commands;
    private HazardCommand currentCommand;
    private bool waiting;
    private float spriteRadius;
    private float commandStartTime;

    void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRadius = (spriteRenderer.bounds.center - spriteRenderer.bounds.size).magnitude;
        this.waiting = false;
    }

    private Vector2 CalculateVectorForce(Vector2 from, Vector2 to, float speed, float mass)
    {
        Vector2 direction = (to - from).normalized;
        float F = mass * (speed / Time.deltaTime);
        return F * direction;
    }

    private IEnumerator Wait(float seconds)
    {
        this.waiting = true;
        yield return new WaitForSeconds(seconds);
        this.waiting = false;
        this.currentCommand = null;
    }

    public void ExecuteCommands(Queue<HazardCommand> commands)
    {
        this.commands = commands;
    }
    
    private void HandleFire(FireCommand command)
    {
        var vectorForce = this.CalculateVectorForce(command.From, (Vector2)command.To, command.Speed,m_RigidBody.mass);
        m_RigidBody.AddForce(vectorForce);
        this.currentCommand = null;
    }

    private void HandleMove(MoveCommand command)
    {

        if(Vector2.Distance(m_RigidBody.position, command.To) <= spriteRadius){
            if(command.StopAtDestination)
                m_RigidBody.velocity = Vector2.zero;
            this.currentCommand = null;
            m_RigidBody.drag = 0;
            return;
        }

        if(Time.time - this.commandStartTime > command.TimeLimit)
        {
            Debug.Log("Time limit reached");
            this.currentCommand = null;
            m_RigidBody.drag = 0;
            return;
        }

        var forceVector = CalculateVectorForce(m_RigidBody.position, command.To, command.Speed * Time.deltaTime, m_RigidBody.mass);

        // slow arrival not working rn
        // if(command.SlowArrival)
        // {
        //     // float distanceSquared = ((m_RigidBody.position - command.To).normalized * command.Speed).sqrMagnitude;
        //     // float slowRadiusSquared = command.SlowArrivalRadius * command.SlowArrivalRadius;
        //     float distanceSquared = Mathf.Pow(Vector2.Distance(m_RigidBody.position, command.To), 2);
        //     float slowRadiusSquared = Mathf.Pow(command.SlowArrivalRadius, 2);
        //     if (distanceSquared < slowRadiusSquared)
        //     {
        //         forceVector = forceVector * Mathf.Sqrt(distanceSquared / slowRadiusSquared) * Time.deltaTime / command.Speed;
        //     }
        // }

        Vector2 steering = (forceVector - m_RigidBody.velocity) / Time.deltaTime;
        Vector2 steerForce = m_RigidBody.mass * steering;
        steerForce = Vector2.ClampMagnitude(forceVector, maxSteeringForce);
        m_RigidBody.AddForce(steerForce);

        // Seek
        // ----------------------
        // Vector2 desiredVel = (to - from).normalized * controller.MaxSpeed;
        // Vector2 steering = (desiredVel - controller.Rigidbody2D.velocity) / Time.deltaTime;  // acceleration effectively
        // Vector2 steerForce = controller.Rigidbody2D.mass * steering;
        // steerForce = Vector2.ClampMagnitude(moveForce, maxSteeringForce);
        // ----------------------


        // ArrivalBehavior
        // ----------------------
        // public static Vector2 ArrivalBehavior(EnemyController controller, GameObject target, float slowRadius)
        // ^^ HEADER
        // Vector2 desiredVel = target.transform.position - controller.transform.position;
        // float distanceSquared = desiredVel.sqrMagnitude;
        // float slowRadiusSquared = slowRadius * slowRadius;

        // if (distanceSquared < slowRadiusSquared)
        // {
        //             desiredVel = desiredVel.normalized * controller.MaxSpeed * Mathf.Sqrt(distanceSquared / slowRadiusSquared);
        // }
        // else // Outside the slowRadius
        // {
        //             desiredVel = desiredVel.normalized * controller.MaxSpeed;
        // }

        // Vector2 steering = desiredVel - controller.Rigidbody2D.velocity; 
        // ----------------------
    }

    private void HandleDragChange(DragChangeCommand command)
    {
        m_RigidBody.drag = command.LinearDrag;
        m_RigidBody.angularDrag = command.AngularDrag;
        this.currentCommand = null;
    }

    private void HandleWait(WaitCommand command)
    {
        if(!this.waiting)
        {
            StartCoroutine(Wait(command.Seconds));
        }
    }

    private void HandleExecuteCommand()
    {
        if(this.currentCommand == null)
        {
            if(this.commands.Count > 0){
                this.currentCommand = this.commands.Dequeue();
                this.commandStartTime = Time.time;
            }
        }

        switch(this.currentCommand)
        {
            case FireCommand f:
            HandleFire(f);
            break;

            case MoveCommand m:
            m_RigidBody.drag = 5;
            HandleMove(m);
            break;

            case WaitCommand w:
            HandleWait(w);
            break;

            case DragChangeCommand d:
            HandleDragChange(d);
            break;

            case null:
            break;
        }
    }
    void FixedUpdate()
    {
        HandleExecuteCommand();
    }

    void Update()
    {
        if(rotate)
        {
            this.transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
