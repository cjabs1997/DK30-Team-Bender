using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : StateController<PlayerState, PlayerStateFactory>
{
    private void Start()
    {
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate(this);
    }

    private void Update()
    {
        currentState.StateUpdate(this);   
    }


    public override void TransitionToState(PlayerState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
