using States;
using UnityEngine;
using Helpers.MovementHelpers;
using System;

[CreateAssetMenu]
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

        Debug.Log("START: " + controller.transform.position.x);

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
            controller.TransitionToState(controller.PlayerStateFactory.GetState(State.fall));
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
        if(Mathf.Abs(controller.Rigidbody2D.velocity.x) <= 0.05f || startDir != Mathf.Sign(controller.Rigidbody2D.velocity.x)) // If we're already moving slow or change directions stop, this is a LITTLE jank :^)
        {
            controller.Rigidbody2D.velocity = new Vector2(0f, controller.Rigidbody2D.velocity.y);
            Debug.Log("Finish: " + controller.transform.position.x);
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
