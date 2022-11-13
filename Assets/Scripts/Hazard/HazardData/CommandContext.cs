using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandContext
{
    private HazardCommand command;
    public HazardCommand Command => command;
    private Vector2 from;
    public Vector2 From => from;
    private Transform toTransform;
    public Transform ToTransform => toTransform;
    private Vector2 to;
    public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position : to ;} }

    private bool waiting;
    public bool Waiting { get; set; }
    private GameObject caller;
    public GameObject Caller => caller;

    public CommandContext(HazardCommand command, Vector2 from, Transform toTransform, GameObject caller)
    {
        this.command = command;
        this.from = from;
        this.toTransform = toTransform;
        this.to = toTransform.position;
        this.caller = caller;
        this.waiting  = false;
    }

    public CommandContext(HazardCommand command, Vector2 from, Vector2 to, GameObject caller)
    {
        this.command = command;
        this.from = from;
        this.to = toTransform.position;
        this.caller = caller;
        this.waiting  = false;
    }

}
