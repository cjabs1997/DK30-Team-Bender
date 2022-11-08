using System.Collections;
using UnityEngine;

public class HazardProjectile : HazardObject
{
    [SerializeField] bool rotate;
    [SerializeField] float degreesPerSecond;
    [SerializeField] float maxSteeringForce;

    [SerializeField] AudioClip SoundOnMove;

    Rigidbody2D m_RigidBody; 
    SpriteRenderer spriteRenderer;


    private bool waiting;
    private float spriteRadius;

    void Start()
    {
        this.m_RigidBody = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.spriteRadius = (spriteRenderer.bounds.center - spriteRenderer.bounds.size).magnitude;
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
        if(Vector2.Distance(m_RigidBody.position, command.To) <= 0.01){
            m_RigidBody.velocity = Vector2.zero;
            this.currentCommand = null;
            m_RigidBody.drag = 0;
            return;
        }

        if(Time.time - this.commandStartTime > command.TimeLimit)
        {
            this.currentCommand = null;
            m_RigidBody.drag = 0;
            return;
        }

        Vector2 forceVector;
        if(command.SlowArrival)
        {
            forceVector = CalculateSlowArrivalVectorForce(m_RigidBody.position, command.To, command.Speed, m_RigidBody.mass, command.SlowArrivalRadius);
        }
        else
        {
            forceVector = CalculateVectorForce(m_RigidBody.position, command.To, command.Speed, m_RigidBody.mass);
        }

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

            if(this.currentCommand is MoveCommand)
            {
                if(this.SoundOnMove != null)
                    {
                        var audio = this.GetComponent<AudioSource>();
                        audio.clip = this.SoundOnMove;
                        audio.Play();
                    }
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



    void OnTriggerEnter2D(Collider2D collider)
    {
        // TODO
        // call collider's function with damage as argument
        // if(collider.GetComponent<HazardProjectile>() == null)
        //     Destroy(this.gameObject);

        // if(collider.TryGetComponent<StateController>(out StateController stateController))
        // {
        //     // stateController.ApplyDamage(float);
        //     // public bool ApplyDamage(float damage);
        // }
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
