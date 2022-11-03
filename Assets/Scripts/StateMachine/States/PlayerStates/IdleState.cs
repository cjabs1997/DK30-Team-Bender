using States;
using UnityEngine;
using Helpers.MovementHelpers;
using System;

[CreateAssetMenu(menuName = "States/Player/Idle")]
public class IdleState : BaseState
{
    public override State stateKey { get { return State.idle; } }
    [SerializeField] protected PlayerStats stats;

    float acceleration;
    float startDir;

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        startDir = Math.Sign(controller.Rigidbody2D.velocity.x);
        acceleration = -Mathf.Pow(controller.Rigidbody2D.velocity.x, 2) / (2 * stats.StopDistance);
        acceleration *= startDir;
    }

    public override void ExitState()
    {
        
    }

    public override void HandleStateTransitions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.jump));
            return;
        }

        // If we aren't touching ground
        if (!MovementHelpers.CheckGround(controller, stats.GroundMask))
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.delayedJump));
            return;
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.move));
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        // If we're already moving slow or change directions stop, this is a LITTLE jank :^)
        if (startDir != Mathf.Sign(controller.Rigidbody2D.velocity.x))
        {
            controller.Rigidbody2D.velocity = new Vector2(0f, controller.Rigidbody2D.velocity.y);
        }
        else
        { 
            controller.Rigidbody2D.AddForce(Vector2.right * acceleration * controller.Rigidbody2D.mass);
        }
        
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void StateUpdate()
    {
        HandleStateTransitions();
    }
}
