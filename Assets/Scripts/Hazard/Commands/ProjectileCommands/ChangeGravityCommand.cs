using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeCommand : HazardCommand
{
    [SerializeField]
    private float gravity;
    public float Gravity => gravity;

    public GravityChangeCommand(float gravity)
    {
        this.gravity = gravity;
    }
}