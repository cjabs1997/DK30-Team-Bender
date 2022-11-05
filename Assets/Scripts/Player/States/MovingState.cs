using UnityEngine;
using Helpers.MovementHelpers;
using States;

/// <summary>
/// State for when the player is moving (or just standing still for now, can split that logic later if needed).
/// </summary>

[CreateAssetMenu(menuName = "States/Player/Moving")]
public class MovingState : PlayerState
{
    public override State stateKey { get { return State.move; } }

    public override void EnterState(PlayerStateController controller)
    {
        base.EnterState(controller);

        controller.Rigidbody2D.drag = 0;
    }

    public override void StateUpdate(PlayerStateController controller)
    {
        HandleStateTransitions(controller);
    }

    public override void StateFixedUpdate(PlayerStateController controller)
    {
        Vector2 moveForce = MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalGroudedForce, stats.MaxSpeed, Input.GetAxisRaw("Horizontal"));

        controller.Rigidbody2D.AddForce(moveForce);
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

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.idle));
            return;
        }
    }
}
