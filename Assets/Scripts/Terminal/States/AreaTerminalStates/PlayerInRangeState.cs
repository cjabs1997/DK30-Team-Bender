using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

[CreateAssetMenu(menuName ="States/Terminal/PlayerInRange")]
public class PlayerInRangeState : AreaTerminalState
{

    public override State stateKey => State.playerInRange;

    public override void EnterState(WaitInAreaTerminalController controller)
    {
        controller.TerminalProgressBar.gameObject.SetActive(true);
    }

    public override void ExitState(WaitInAreaTerminalController controller)
    {
        
    }

    public override void HandleStateTransitions(WaitInAreaTerminalController controller)
    { 
        if(controller.Progress >= controller.Terminal.TimeToComplete)
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.terminalComplete));
            // Among other things
        }
    }


    public override void StateOnTriggerExit2D(WaitInAreaTerminalController controller, Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.playerOutOfRange));
        }
    }

    public override void StateUpdate(WaitInAreaTerminalController controller)
    {
        controller.Progress += Time.deltaTime;

        controller.TerminalProgressBar.UpdateFill(Mathf.Min(controller.Progress / controller.Terminal.TimeToComplete));
        HandleStateTransitions(controller);
    }

}
