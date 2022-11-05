using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardProjectile : HazardObject
{
    [SerializeField] bool rotate;
    [SerializeField] float degreesPerSecond;
    [SerializeField] float maxSteeringForce;

    Rigidbody2D m_RigidBody; 
    SpriteRenderer spriteRenderer;


    private bool waiting;
    private float spriteRadius;

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

    private Vector2 CalculateSlowArrivalVectorForce(Vector2 from, Vector2 to, float speed, float mass, float radius)
    {
        Vector2 desiredVel = from - to;
        float distanceSquared = desiredVel.sqrMagnitude;
        float slowRadiusSquared = radius * radius;

        if (distanceSquared < slowRadiusSquared)
        {
                    desiredVel = desiredVel.normalized * speed * Mathf.Sqrt(distanceSquared / slowRadiusSquared);
        }
        else // Outside the slowRadius
        {
                    desiredVel = desiredVel.normalized * speed;
        }

        return -desiredVel;
    }
    private IEnumerator Wait(float seconds)
    {
        this.waiting = true;
        yield return new WaitForSeconds(seconds);
        this.waiting = false;
        this.currentCommand = null;
    }

    
    private void HandleFire(FireCommand command)
    {
        var vectorForce = this.CalculateVectorForce(command.From, (Vector2)command.To, command.Speed,m_RigidBody.mass);
        m_RigidBody.AddForce(vectorForce);
        this.currentCommand = null;
    }

    private void HandleMove(MoveCommand command)
    {
        Debug.Log("Handling Move");
        if(Vector2.Distance(m_RigidBody.position, command.To) <= 0.01){
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

        Vector2 forceVector = new Vector2(0, 0);
        // Vector2 forceVector = CalculateVectorForce(m_RigidBody.position, command.To, command.Speed, m_RigidBody.mass);
        if(command.SlowArrival)
        {
            forceVector = CalculateSlowArrivalVectorForce(m_RigidBody.position, command.To, command.Speed, m_RigidBody.mass, command.SlowArrivalRadius);
        }
        else
        {
            forceVector = CalculateVectorForce(m_RigidBody.position, command.To, command.Speed, m_RigidBody.mass);
        }

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

        // public static Vector2 ArrivalBehavior(EnemyController controller, GameObject target, float slowRadius)
        //         ^^ HEADER
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

        Vector2 steering = (forceVector - m_RigidBody.velocity) * Time.deltaTime;
        Vector2 steerForce = m_RigidBody.mass * steering;
        steerForce = Vector2.ClampMagnitude(forceVector, maxSteeringForce);
        m_RigidBody.AddForce(steerForce);

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

    private void HandleGravityChange(GravityChangeCommand command)
    {
        m_RigidBody.gravityScale = command.Gravity;
        this.currentCommand = null;
    }

    protected override void HandleExecuteCommand()
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

            case GravityChangeCommand g:
            HandleGravityChange(g);
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
