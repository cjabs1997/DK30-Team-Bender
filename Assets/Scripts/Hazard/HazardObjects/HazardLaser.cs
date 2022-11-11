using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using LaserCommands;

public class HazardLaser : HazardObject
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D lineCollider;
    private RaycastHit2D castedObject;
    [SerializeField]
    private float laserDistance;
    public float LaserDistance => laserDistance;
    private float angleAmount = 0;
    private bool waiting = false;

    private float getCastedDistance()
    {
        float castedDistance;
        RaycastHit2D castedObject = Physics2D.Raycast(transform.position, transform.up, laserDistance);
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

    private Vector2 getDirectionVector(float angle, bool isRadians=false){
        if(!isRadians)
        {
            angle *= Mathf.Rad2Deg;
        }
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }

    private void castLaser()
    {
        float castedDistance = getCastedDistance();
        lineRenderer.SetPosition(1, Vector2.up * castedDistance);
        if(!lineRenderer.enabled)
            lineRenderer.enabled = true;
        Debug.Log(lineRenderer.GetPosition(1));
        lineCollider.SetPoints(new List<Vector2> { lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) });
    }

    private IEnumerator Wait(float seconds)
    {
        this.waiting = true;
        yield return new WaitForSeconds(seconds);
        this.waiting = false;
        this.currentCommand = null;
    }
    private void HandleWait(WaitCommand command)
    {
        if(!this.waiting)
        {
            StartCoroutine(Wait(command.Seconds));
        }
    }

    void HandleFire(ToggleActivation command)
    {
        if(command.Activate)
        {
            castLaser();
        }
        else
        {
            Destroy(gameObject);
        }
        this.currentCommand = null;
    }

    void HandleMove(MoveBy command)
    {

        if(this.lineRenderer == null)
        {
            this.transform.Rotate(xAngle: 0, 0, command.Angle);
            this.castLaser();
            this.currentCommand = null;
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
            this.currentCommand = null;
        this.transform.Rotate(xAngle: 0, 0, angleDelta);
        this.castLaser();
        
    }

    override protected void HandleExecuteCommand()
    {
        if(this.currentCommand == null)
        {
            if(this.commands.Count > 0)
            {
                this.currentCommand = this.commands.Dequeue();
                this.commandStartTime = Time.time;
                if(this.currentCommand is MoveBy)
                {
                    this.angleAmount = ((MoveBy) this.currentCommand).Angle;
                }
            }
        }

        switch(this.currentCommand)
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
    }

    void FixedUpdate()
    {
        HandleExecuteCommand();
    }
    
}
