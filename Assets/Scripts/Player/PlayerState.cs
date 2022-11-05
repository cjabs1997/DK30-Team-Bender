using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class PlayerState : BaseState<PlayerStateController>
{
    public override State stateKey => throw new System.NotImplementedException(); // This "SHOULD" never get called but we'll throw one just in case

    [SerializeField] protected PlayerStats stats;
}
