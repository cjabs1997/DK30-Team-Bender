using States;
using UnityEngine;
using Helpers.MovementHelpers;

/// <summary>
/// State for when you are falling, effectively only under the effects of gravity
/// </summary>

[CreateAssetMenu(menuName = "States/Player/Falling")]
public class FallingState : PlayerState
{
    public override State stateKey { get { return State.fall; } }

    public override void EnterState(PlayerStateController controller)
    {
        base.EnterState(controller);

        //controller.Rigidbody2D.drag = 0;
    }

    public override void HandleStateTransitions(PlayerStateController controller)
    {
        if (MovementHelpers.CheckGround(controller.Collider2D, stats.GroundMask))
        {
            controller.Rigidbody2D.AddForce(MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalForceInAir * 4.5f, stats.MaxSpeed, Input.GetAxisRaw("Horizontal"))); // This is pretty jank solution 
            controller.TransitionToState(controller.StateFactory.GetState(State.move));
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
        if(Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(controller.Rigidbody2D.velocity.x) || Mathf.Abs(controller.Rigidbody2D.velocity.x) < stats.MaxSpeed)
        {
            moveForce.x = MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalForceInAir, stats.MaxSpeed, Input.GetAxisRaw("Horizontal")).x;
        }

        controller.Rigidbody2D.AddForce(moveForce);

    }

    public override void StateUpdate(PlayerStateController controller)
    {
        HandleStateTransitions(controller);
    }
}
