using States;
using UnityEngine;
using System.Collections;
using Helpers.MovementHelpers;

/// <summary>
/// State for when you are holding down the jump button. Idea is you should keep rising while you are holding the button
/// </summary>

[CreateAssetMenu]
public class JumpingState : PlayerState
{
    public override State stateKey { get { return State.jump; } }

    private Coroutine maxJumpRoutine;

    public override void EnterState(PlayerStateController controller)
    {
        base.EnterState(controller);
        
        controller.Rigidbody2D.drag = 0;
        controller.Rigidbody2D.AddForce(Vector2.up * stats.InitialJump, ForceMode2D.Impulse); // Adding an upward force, I don't like how this translates but it works for now
        maxJumpRoutine = controller.StartCoroutine(MaxJump(controller));
       
    }

    public override void HandleStateTransitions(PlayerStateController controller)
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            controller.StopCoroutine(maxJumpRoutine);
            controller.TransitionToState(controller.StateFactory.GetState(State.fall));
            return;
        }
    }

    public override void StateFixedUpdate(PlayerStateController controller)
    {
        if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(controller.Rigidbody2D.velocity.x) || Mathf.Abs(controller.Rigidbody2D.velocity.x) < stats.MaxSpeed)
        {
            controller.Rigidbody2D.AddForce(MovementHelpers.LateralMove(controller.Rigidbody2D, stats.MaxHorizontalForceInAir, stats.MaxSpeed, Input.GetAxisRaw("Horizontal")));
        }
    }

    public override void StateUpdate(PlayerStateController controller)
    {
        HandleStateTransitions(controller);
    }
    
    IEnumerator MaxJump(PlayerStateController controller)
    {
        yield return new WaitForSeconds(stats.MaxJumpTime);
        controller.TransitionToState(controller.StateFactory.GetState(State.fall));
    }
    
}
