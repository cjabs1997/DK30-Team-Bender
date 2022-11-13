using System.Collections;
using UnityEngine;
using ProjectileCommands;
public class HazardProjectile : HazardObject
{
    [SerializeField] bool rotate;
    [SerializeField] float degreesPerSecond;
    [SerializeField] float maxSteeringForce;

    [SerializeField]
    private SimpleAudioEvent soundOnMove;
    public SimpleAudioEvent SoundOnMove => soundOnMove;
    [SerializeField]
    private LayerMask collisionLayers;
    public LayerMask CollisionLayers => collisionLayers;

    Rigidbody2D m_RigidBody; 
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    private float spriteRadius;

    void Start()
    {
        this.m_RigidBody = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSource = this.GetComponent<AudioSource>();
        this.spriteRadius = (spriteRenderer.bounds.center - spriteRenderer.bounds.size).magnitude;
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
        this.currentCommandContext.Waiting = true;
        yield return new WaitForSeconds(seconds);
        this.currentCommandContext.Waiting = false;
        this.currentCommandContext = null;
    }

    private void HandleWait(WaitCommand command)
    {
        if(!this.currentCommandContext.Waiting)
        {
            StartCoroutine(Wait(command.Seconds));
        }
    }
    
    private void HandleFire(FireCommand command)
    {
        var vectorForce = this.CalculateVectorForce(m_RigidBody.position, (Vector2)this.currentCommandContext.To, command.Speed,m_RigidBody.mass);
        this.transform.rotation = Quaternion.FromToRotation(Vector2.left, vectorForce);
        m_RigidBody.AddForce(vectorForce);
        this.currentCommandContext = null;
    }

    private void HandleMove(MoveCommand command)
    {
        if(Vector2.Distance(m_RigidBody.position, this.currentCommandContext.To) <= 0.01){
            m_RigidBody.velocity = Vector2.zero;
            this.currentCommandContext = null;
            m_RigidBody.drag = 0;
            return;
        }

        if(Time.time - this.commandStartTime > command.TimeLimit)
        {
            this.currentCommandContext = null;
            m_RigidBody.drag = 0;
            return;
        }
        Vector2 forceVector;
        if(command.SlowArrival)
        {
            forceVector = CalculateSlowArrivalVectorForce(m_RigidBody.position, this.currentCommandContext.To, command.Speed, m_RigidBody.mass, command.SlowArrivalRadius);
        }
        else
        {
            forceVector = CalculateVectorForce(m_RigidBody.position, this.currentCommandContext.To, command.Speed, m_RigidBody.mass);
        }
        Vector2 steering = (forceVector - m_RigidBody.velocity) * Time.deltaTime;
        Vector2 steerForce = m_RigidBody.mass * steering;
        steerForce = Vector2.ClampMagnitude(forceVector, maxSteeringForce);
        m_RigidBody.AddForce(steerForce);
    }

    private void HandleDragChange(ChangeDragCommand command)
    {
        m_RigidBody.drag = command.LinearDrag;
        m_RigidBody.angularDrag = command.AngularDrag;
        this.currentCommandContext = null;
    }


    private void HandleGravityChange(ChangeGravityCommand command)
    {
        m_RigidBody.gravityScale = command.Gravity;
        this.currentCommandContext = null;
    }
    
    protected override void HandleExecuteCommand()
    {
        if(this.currentCommandContext == null)
        {
            if(this.commandContexts.Count > 0){
                this.currentCommandContext = this.commandContexts.Dequeue();
                this.commandStartTime = Time.time;
            
                if(this.currentCommandContext.Command is MoveCommand || this.currentCommandContext.Command is FireCommand)
                {
                    if(this.SoundOnMove != null)
                    {
                        this.SoundOnMove.Play(this.audioSource);
                    }
                }
            }
        }

        if(this.currentCommandContext == null) return;

        switch(this.currentCommandContext.Command)
        {
            case FireCommand f:
            HandleFire(f);
            break;

            case MoveCommand m:
            m_RigidBody.drag = 5;
            HandleMove(command: m);
            break;

            case WaitCommand w:
            HandleWait(w);
            break;

            case ChangeDragCommand d:
            HandleDragChange(d);
            break;

            case ChangeGravityCommand g:
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
        Debug.Log("Hit");
        Debug.Log(collider);
        if(collider.IsTouchingLayers(this.collisionLayers))
        {
            Destroy(this.gameObject);
        }
        // TODO
        // call collider's function with damage as argument
        // if(collider.GetComponent<HazardProjectile>() == null)
        //     Destroy(this.gameObject);

        // if(collider.TryGetComponent<PlayerStateController>(out PlayerStateController stateController))
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
        // TODO make this based on outside scene, not camera
        Destroy(gameObject);
    }
}
