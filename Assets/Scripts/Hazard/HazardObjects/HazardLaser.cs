using System.Collections.Generic;
using UnityEngine;

public class HazardLaser : HazardObject
{
    LineRenderer lineRenderer;
    EdgeCollider2D lineCollider;
    private RaycastHit2D castedObject;
    [SerializeField]
    private float laserDistance;
    public float LaserDistance => laserDistance;

    private void initializeLaser(Vector2 from, Vector2 to)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.SetPositions(new Vector3[2] { from, to });

        lineCollider = GetComponent<EdgeCollider2D>();
        lineCollider.SetPoints(new List<Vector2> { from, to });
    }

    void HandleFire(FireCommand command)
    {
        if(command.Speed > 0)
        {
            initializeLaser(command.From, command.To);
        }
        else
        {
            Destroy(gameObject);
        }
        this.currentCommand = null;

    }

    void HandleMove(MoveCommand command)
    {
        if(this.lineRenderer == null)
        {
            this.initializeLaser(command.From, command.To);
            currentCommand = null;
            return;
        }

        var p1 = command.To;
        var p2 = (Vector2) lineRenderer.GetPosition(1);
        var origin = command.From;
        // https://www.mathworks.com/matlabcentral/answers/180131-how-can-i-find-the-angle-between-two-vectors-including-directional-information
        var angle = Mathf.Atan2(p1.y*p2.x - p1.x*p2.y, p1.x*p2.x+p1.y*p2.y);

        angle = Mathf.Clamp(angle, -command.Speed * Time.deltaTime, command.Speed * Time.deltaTime);

        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);

        p2 -= origin;
        var newP2 = new Vector2(
            p2.x * c - p2.y * s,
            p2.x * s + p2.y * c
        );

        castedObject = Physics2D.Raycast(command.From, newP2, laserDistance);
        if(castedObject.collider != null)
        {
            newP2 = newP2 * ( castedObject.point.magnitude / newP2.magnitude);
        }
        else
        {
            newP2 = newP2 * (laserDistance / newP2.magnitude);
        }

        newP2 += origin;
        lineRenderer.SetPosition(1, newP2);

        lineCollider.SetPoints(new List<Vector2> { lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) });

        if(Mathf.Abs(angle) < command.Speed * Time.deltaTime)
            this.currentCommand = null;

    }

    override protected void HandleExecuteCommand()
    {
        if(this.currentCommand == null)
        {
            if(this.commands.Count > 0)
            {
                this.currentCommand = this.commands.Dequeue();
                this.commandStartTime = Time.time;
            }
        }
        switch(this.currentCommand)
        {
            case MoveCommand c:
            HandleMove(c);
            break;

            case FireCommand f:
            HandleFire(f);
            break;

            case null:
            break;
        }
    }

    void FixedUpdate()
    {
        HandleExecuteCommand();
    }
    
}
