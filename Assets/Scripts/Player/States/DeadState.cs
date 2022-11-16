using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;
using Helpers.MovementHelpers;

[CreateAssetMenu(menuName = "States/Player/Dead")]
public class DeadState : PlayerState
{
    public override State stateKey => State.dead;

    private bool _grounded { get; set; } //  Also not great but it is what it is!

    public override void EnterState(PlayerStateController controller)
    {
        _grounded = MovementHelpers.CheckGround(controller.Collider2D, stats.GroundMask);
        controller.SpriteRenderer.color = Color.black;
        controller.SpriteRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
        if(_grounded)
            controller.Rigidbody2D.velocity = new Vector2(0, controller.Rigidbody2D.velocity.y);
    }

    // Update is called once per frame
    public override void StateUpdate(PlayerStateController controller)
    {
        if(!_grounded && MovementHelpers.CheckGround(controller.Collider2D, stats.GroundMask))
        {
            _grounded = true;
            controller.Rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void StateFixedUpdate(PlayerStateController controller)
    {
        if(!_grounded)
        {
            KeepFalling(controller);
        }
        
    }

    private void KeepFalling(PlayerStateController controller)
    {
        Vector2 moveForce = Vector2.zero;

        // Apply gravity force unless we've reached terminal velocity
        if (controller.Rigidbody2D.velocity.y >= -stats.TerminalVelocity)
        {
            moveForce = Vector2.down * stats.Gravity;
        }

        controller.Rigidbody2D.AddForce(moveForce);
    }
}
