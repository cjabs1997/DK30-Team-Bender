using States;
using UnityEngine;
using Helpers.MovementHelpers;
using System;
using System.Collections;

[CreateAssetMenu(menuName ="States/Player/DelayedJump")]
public class DelayedJumpState : BaseState
{
    [SerializeField] protected PlayerStats stats; // This is bad, I'll make this better later once architecture is more laid out

    public override State stateKey { get { return State.delayedJump; } }

    private Coroutine CanJumpRoutine;

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        CanJumpRoutine = controller.StartCoroutine(CanJump());
    }

    public override void ExitState()
    {
        
    }

    public override void HandleStateTransitions()
    {
        if (MovementHelpers.CheckGround(controller, stats.GroundMask))
        {
            controller.Rigidbody2D.AddForce(MovementHelpers.LateralMove(controller, stats.MaxHorizontalForceInAir * 4.5f, stats.MaxSpeed, Input.GetAxisRaw("Horizontal"))); // This is pretty jank solution 
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.move));
            controller.StopCoroutine(CanJumpRoutine);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.jump));
            controller.StopCoroutine(CanJumpRoutine);
            return;
        }
    }

    public override void StateFixedUpdate()
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
            moveForce.x = MovementHelpers.LateralMove(controller, stats.MaxHorizontalForceInAir, stats.MaxSpeed, Input.GetAxisRaw("Horizontal")).x;
        }

        controller.Rigidbody2D.AddForce(moveForce);
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
       
    }

    public override void StateUpdate()
    {
        HandleStateTransitions();
    }

    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(stats.JumpDelay);

        controller.TransitionToState(controller.PlayerStateFactory.GetState(State.fall));
    }
}
