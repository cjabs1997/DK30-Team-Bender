using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

[CreateAssetMenu]
public class InTerminalState : BaseState
{
    public override State stateKey => State.inTerminal;

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        controller.Rigidbody2D.velocity = Vector2.zero;
    }

    public override void ExitState()
    {
        
    }

    public override void HandleStateTransitions()
    {
        
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void StateUpdate()
    {
        
    }
}
