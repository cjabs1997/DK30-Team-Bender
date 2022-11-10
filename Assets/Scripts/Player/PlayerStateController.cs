using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : StateController<PlayerState, PlayerStateFactory>
{
    public Terminal CurrentTerminal { get; set; }

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

        if (Input.GetKeyDown(KeyCode.E) && CurrentTerminal != null)
        {
            CurrentTerminal.ActivateTerminal();
            CurrentTerminal = null;
        }
    }


    public override void TransitionToState(PlayerState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
