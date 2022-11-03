using States;
using UnityEngine;
using Helpers.MovementHelpers;

/// <summary>
/// State for when you are falling, effectively only under the effects of gravity
/// </summary>

[CreateAssetMenu(menuName = "States/Player/Falling")]
public class FallingState : BaseState
{
    [SerializeField] protected PlayerStats stats; // This is bad, I'll make this better later once architecture is more laid out

    public override State stateKey { get { return State.fall; } }

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        //controller.Rigidbody2D.drag = 0;
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
        if(Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(controller.Rigidbody2D.velocity.x) || Mathf.Abs(controller.Rigidbody2D.velocity.x) < stats.MaxSpeed)
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
}
