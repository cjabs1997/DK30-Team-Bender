using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using LaserCommands;

public class HazardLaser : HazardObject
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D lineCollider;
    private RaycastHit2D castedObject;
    private AudioSource audioSource;
    [SerializeField]
    private float laserDistance;
    public float LaserDistance => laserDistance;
    [SerializeField]
    private AudioClip soundWhileOn;
    public AudioClip SoundWhileOn => soundWhileOn;
    [SerializeField]
    private LayerMask raycastLayerMask;
    public LayerMask RaycastLayerMask => raycastLayerMask;

    private float getCastedDistance()
    {
        float castedDistance;
        RaycastHit2D castedObject = Physics2D.Raycast(transform.position, transform.up, laserDistance, raycastLayerMask);
        if(castedObject.collider != null)
        {
            castedDistance = Vector2.Distance(castedObject.point, transform.position);
        }
        else
        {
            castedDistance = laserDistance;
        }
        return castedDistance;
    }

    private void castLaser()
    {
        float castedDistance = getCastedDistance();
        lineRenderer.SetPosition(1, Vector2.up * castedDistance);
        if(!lineRenderer.enabled)
            lineRenderer.enabled = true;
        lineCollider.SetPoints(new List<Vector2> { lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) });
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

    void HandleFire(ToggleActivation command)
    {
        if(command.Activate)
        {
            if(this.audioSource)
            {
                this.audioSource.clip = this.soundWhileOn;
                this.audioSource.Play();
            }
            castLaser();
        }
        else
        {
            this.audioSource.Stop();
            Destroy(gameObject);
        }
        this.currentCommandContext = null;
    }

    void HandleMove(MoveBy command)
    {

        if(this.lineRenderer == null)
        {
            this.transform.Rotate(xAngle: 0, 0, command.Angle);
            this.castLaser();
            this.currentCommandContext = null;
            return;
        }
        
        var currentAngle = MoveBy.GetSignedAngle(Vector2.up, transform.up);
        var commandAngle = command.Angle;
        float angleDiff = currentAngle - commandAngle;

        if(angleDiff > 180)
            angleDiff = -(360 - angleDiff);

        if(angleDiff < -180)
            angleDiff = -(-360 - angleDiff);

        var angleDelta = Mathf.Clamp(angleDiff, -command.Speed * Time.deltaTime, command.Speed * Time.deltaTime);
        if(angleDiff*angleDiff <= angleDelta*angleDelta)
            this.currentCommandContext = null;
        this.transform.Rotate(xAngle: 0, 0, angleDelta);
        this.castLaser();
        
    }

    override protected void HandleExecuteCommand()
    {
        if(this.currentCommandContext == null)
        {
            if(this.commandContexts.Count > 0)
            {
                this.currentCommandContext = this.commandContexts.Dequeue();
                this.commandStartTime = Time.time;
            }
        }

        switch(this.currentCommandContext.Command)
        {
            case MoveBy c:
            HandleMove(c);
            break;

            case ToggleActivation f:
            HandleFire(f);
            break;

            case WaitCommand w:
            HandleWait(w);
            break;

            case null:
            break;
        }
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.SetPositions(new Vector3[2] { Vector2.zero, Vector2.up});
        lineCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.alignment = LineAlignment.TransformZ;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        HandleExecuteCommand();
    }
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log("Hit");
        Debug.Log(collider);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit");
        Debug.Log(collider);
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
}
