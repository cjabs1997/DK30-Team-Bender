using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/SimpleTerminal/Unactivated")]
public class SimpleUnactivatedState : SimpleTerminalState
{
    public override States.State stateKey => States.State.playerOutOfRange; // Bad name, didn't feel like adding a new one :P

    public override void EnterState(SimpleTerminalController controller)
    {
        controller.SpriteRenderer.color = Color.white;
    }

    public override void StateOnTriggerEnter2D(SimpleTerminalController controller, Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStateController c))
        {
            c.CurrentTerminal = controller.Terminal;
        }
    }

    public override void StateOnTriggerExit2D(SimpleTerminalController controller, Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStateController c))
        {
            c.CurrentTerminal = null;
        }
    }
}
