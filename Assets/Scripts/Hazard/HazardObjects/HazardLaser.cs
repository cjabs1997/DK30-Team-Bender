using System.Collections.Generic;
using UnityEngine;

public class HazardLaser : HazardObject
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D lineCollider;
    private RaycastHit2D castedObject;
    [SerializeField]
    private float laserDistance;
    public float LaserDistance => laserDistance;

    private float getCastedLocationScale(Vector2 from, Vector2 to)
    {
        float castedScale;
        float diffMagnitude = (to - from).magnitude;
        RaycastHit2D castedObject = Physics2D.Raycast(from, transform.up, laserDistance);
        if(castedObject.collider != null)
        {
            castedScale = (from - castedObject.point).magnitude / diffMagnitude;
        }
        else
        {
            castedScale = laserDistance / diffMagnitude;
        }
        return castedScale;
    }

    private void initializeLaser(Vector2 from, Vector2 to)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.alignment = LineAlignment.TransformZ;
        float castedScale = getCastedLocationScale(from, to);
        lineRenderer.SetPositions(new Vector3[2] { from, to * castedScale});

        lineCollider.SetPoints(new List<Vector2> { from, to * castedScale });
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
        Vector2 p2 = transform.TransformPoint(lineRenderer.GetPosition(1));
        var origin = command.From;
        // https://www.mathworks.com/matlabcentral/answers/180131-how-can-i-find-the-angle-between-two-vectors-including-directional-information
        var angle = Mathf.Atan2(p1.y*p2.x - p1.x*p2.y, p1.x*p2.x+p1.y*p2.y);
        angle *= Mathf.Rad2Deg;
        if(Mathf.Abs(angle) < command.Speed * Time.deltaTime)
            this.currentCommand = null;
        angle = Mathf.Clamp(angle, -command.Speed * Time.deltaTime, command.Speed * Time.deltaTime);

        float scale = getCastedLocationScale(command.From, p2);
        Vector2 newLineRendPos = lineRenderer.GetPosition(1) * scale;
        lineRenderer.SetPosition(1, newLineRendPos);
        lineCollider.SetPoints(new List<Vector2> { lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) });

        this.transform.Rotate(new Vector3(0, 0, angle ));
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
