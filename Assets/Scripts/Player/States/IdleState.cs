using States;
using UnityEngine;
using Helpers.MovementHelpers;
using System;

[CreateAssetMenu(menuName = "States/Player/Idle")]
public class IdleState : PlayerState
{
    public override State stateKey { get { return State.idle; } }

    float acceleration;
    float startDir;

    public override void EnterState(PlayerStateController controller)
    {
        base.EnterState(controller);

        startDir = Math.Sign(controller.Rigidbody2D.velocity.x);
        acceleration = -Mathf.Pow(controller.Rigidbody2D.velocity.x, 2) / (2 * stats.StopDistance);
        acceleration *= startDir;
    }

    public override void HandleStateTransitions(PlayerStateController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.jump));
            return;
        }

        // If we aren't touching ground
        if (!MovementHelpers.CheckGround(controller.Collider2D, stats.GroundMask))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.delayedJump));
            return;
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.move));
            return;
        }
    }

    public override void StateFixedUpdate(PlayerStateController controller)
    {
        // If we're already moving slow or change directions stop, this is a LITTLE jank :^)
        if (startDir != Mathf.Sign(controller.Rigidbody2D.velocity.x))
        {
            controller.Rigidbody2D.velocity = new Vector2(0f, controller.Rigidbody2D.velocity.y);
        }
        else if(controller.Rigidbody2D.velocity.x != 0)
        { 
            controller.Rigidbody2D.AddForce(Vector2.right * acceleration * controller.Rigidbody2D.mass);
        }
        
    }

    public override void StateUpdate(PlayerStateController controller)
    {
        HandleStateTransitions(controller);
    }
}
