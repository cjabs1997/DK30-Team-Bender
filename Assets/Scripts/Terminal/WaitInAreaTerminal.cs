using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInAreaTerminal : Terminal
{
    [SerializeField] private float _timeToComplete;
    public float TimeToComplete => _timeToComplete;
}
