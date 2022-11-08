using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[CreateAssetMenu(menuName = "States/Terminal/PlayerOutOfRange")]
public class PlayerOutOfRangeState : AreaTerminalState
{
    public override State stateKey => State.playerOutOfRange;

    public override void EnterState(WaitInAreaTerminalController controller)
    {
        Debug.Log("ENTERED OUT OF RANGE");
        controller.TerminalProgressBar.gameObject.SetActive(false);
    }

    public override void StateOnTriggerEnter2D(WaitInAreaTerminalController controller, Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.playerInRange));
        }
    }
}
