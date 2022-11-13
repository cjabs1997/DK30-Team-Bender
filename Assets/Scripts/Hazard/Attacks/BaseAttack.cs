using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommands : ScriptableObject
{
    public virtual HazardCommand[] Commands { get; protected set; }
}
