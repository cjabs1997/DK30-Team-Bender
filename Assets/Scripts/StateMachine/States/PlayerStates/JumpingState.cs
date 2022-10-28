using States;
using UnityEngine;
using System.Collections;
using Helpers.MovementHelpers;

/// <summary>
/// State for when you are holding down the jump button. Idea is you should keep rising while you are holding the button
/// </summary>

[CreateAssetMenu]
public class JumpingState : BaseState
{
    [SerializeField] protected PlayerStats stats; // This is bad, I'll make this better later once architecture is more laid out

    public override State stateKey { get { return State.jump; } }

    private Coroutine maxJumpRoutine;

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);
        
        controller.Rigidbody2D.drag = 0;
        controller.Rigidbody2D.AddForce(Vector2.up * stats.InitialJump, ForceMode2D.Impulse); // Adding an upward force, I don't like how this translates but it works for now
        maxJumpRoutine = controller.StartCoroutine(MaxJump());
       
    }

    public override void ExitState()
    {

    }

    public override void HandleStateTransitions()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            controller.StopCoroutine(maxJumpRoutine);
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.fall));
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(controller.Rigidbody2D.velocity.x) || Mathf.Abs(controller.Rigidbody2D.velocity.x) < stats.MaxSpeed)
        {
            controller.Rigidbody2D.AddForce(MovementHelpers.LateralMove(controller, stats.MaxHorizontalForceInAir, stats.MaxSpeed, Input.GetAxisRaw("Horizontal")));
        }
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void StateUpdate()
    {

    }
    
    IEnumerator MaxJump()
    {
        yield return new WaitForSeconds(stats.MaxJumpTime);
        controller.TransitionToState(controller.PlayerStateFactory.GetState(State.fall));
    }
    
}
