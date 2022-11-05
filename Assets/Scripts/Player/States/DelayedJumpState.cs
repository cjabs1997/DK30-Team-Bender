using States;
using UnityEngine;
using Helpers.MovementHelpers;
using System;
using System.Collections;

[CreateAssetMenu(menuName ="States/Player/DelayedJump")]
public class DelayedJumpState : PlayerState
{
    public override State stateKey { get { return State.delayedJump; } }

    private Coroutine CanJumpRoutine;

    public override void EnterState(PlayerStateController controller)
    {
        CanJumpRoutine = controller.StartCoroutine(CanJump(controller));
    }

    public override void HandleStateTransitions(PlayerStateController controller)
    {
        if (MovementHelpers.CheckGround(controller.Collider2D, stats.GroundMask))
        {
            controller.Rigidbody2D.AddForce(MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalForceInAir * 4.5f, stats.MaxSpeed, Input.GetAxisRaw("Horizontal"))); // This is pretty jank solution 
            controller.TransitionToState(controller.StateFactory.GetState(State.move));
            controller.StopCoroutine(CanJumpRoutine);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.jump));
            controller.StopCoroutine(CanJumpRoutine);
            return;
        }
    }

    public override void StateFixedUpdate(PlayerStateController controller)
    {
        Vector2 moveForce = Vector2.zero;

        // Apply gravity force unless we've reached terminal velocity
        if (controller.Rigidbody2D.velocity.y >= -stats.TerminalVelocity)
        {
            moveForce = Vector2.down * stats.Gravity;
        }

        // 
        if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(controller.Rigidbody2D.velocity.x) || Mathf.Abs(controller.Rigidbody2D.velocity.x) < stats.MaxSpeed)
        {
            moveForce.x = MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalForceInAir, stats.MaxSpeed, Input.GetAxisRaw("Horizontal")).x;
        }

        controller.Rigidbody2D.AddForce(moveForce);
    }

    public override void StateUpdate(PlayerStateController controller)
    {
        HandleStateTransitions(controller);
    }

    IEnumerator CanJump(PlayerStateController controller)
    {
        yield return new WaitForSeconds(stats.JumpDelay);

        controller.TransitionToState(controller.StateFactory.GetState(State.fall));
    }
}
