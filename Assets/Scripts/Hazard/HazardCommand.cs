using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HazardCommandType
{
    MOVE,
    WAIT,
    FIRE
}

public abstract class HazardCommand {}

public class MoveCommand : HazardCommand 
{
    private Vector2 from;
    public Vector2 From => from;
    private Vector2 to;
    public Vector2 To => to;
    private float speed;
    public float Speed => speed;

    public MoveCommand(Vector2 from, Vector2 to, float speed)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
    }

}

public class WaitCommand : HazardCommand
{
    private float seconds;
    public float Seconds => seconds;

    public WaitCommand(float seconds)
    {
        this.seconds = seconds;
    }

}

public class FireCommand : HazardCommand
{
    private Vector2 from;
    public Vector2 From => from;
    private Transform to;
    public Transform To => to;

    private float speed;
    public float Speed => speed;

    public FireCommand(Vector2 from, Transform to, float speed)
    {
        this.from = from;
        this.to = to;
        this.speed = speed;
    }

}