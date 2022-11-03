using UnityEngine;
using Helpers.MovementHelpers;
using States;

/// <summary>
/// State for when the player is moving (or just standing still for now, can split that logic later if needed).
/// </summary>

[CreateAssetMenu(menuName = "States/Player/Moving")]
public class MovingState : BaseState
{
    [SerializeField] protected PlayerStats stats; // This is bad, I'll make this better later once architecture is more laid out

    public override State stateKey { get { return State.move; } }

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        controller.Rigidbody2D.drag = 0;
    }

    public override void StateUpdate()
    {
        HandleStateTransitions();
    }

    public override void StateFixedUpdate()
    {
        Vector2 moveForce = MovementHelpers.LateralMove(controller, stats.MaxHorizontalGroudedForce, stats.MaxSpeed, Input.GetAxisRaw("Horizontal"));

        controller.Rigidbody2D.AddForce(moveForce);
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

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.idle));
            return;
        }
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void ExitState()
    {
        
    }
}
