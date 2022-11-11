using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : HazardCommand
{
    [SerializeField]
    private float seconds;
    public float Seconds => seconds;

    public WaitCommand(float seconds)
    {
        this.seconds = seconds;
    }

}
