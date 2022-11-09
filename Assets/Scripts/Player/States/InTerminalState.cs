using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class InTerminalState : ScriptableObject
{
    /*
     * IDK IF WE're GONNA USE THIS ANYNMORE SO JUST COMMENTING IT ALL OUT FOR NOW
    public override State stateKey => State.inTerminal;

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        controller.Rigidbody2D.velocity = Vector2.zero;
        controller.currentTerminal.OpenTerminal();
    }

    public override void ExitState()
    {
        
    }

    public override void HandleStateTransitions()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || controller.currentTerminal == null)
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.idle));
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void StateUpdate()
    {
        HandleStateTransitions();   
    }
    */
}
