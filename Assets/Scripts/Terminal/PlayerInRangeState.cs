using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

[CreateAssetMenu(menuName ="States/Terminal/PlayerInRange")]
public class PlayerInRangeState : MonoBehaviour
{
    /*
    public override State stateKey => State.playerInRange;

    [SerializeField] private float _timeToComplete;

    public override void EnterState(WaitInAreaTerminal controller)
    {


        controller = controller.GetComponent<WaitInAreaTerminal>();
    }

    public override void ExitState(WaitInAreaTerminal controller)
    {
        
    }

    public override void HandleStateTransitions(WaitInAreaTerminal controller)
    { 
        if(controller.Progress >= 1f)
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.completed));
            // Among other things
        }
    }

    public override void StateFixedUpdate(WaitInAreaTerminal controller)
    {
           
    }

    public override void StateOnCollisionEnter2D(WaitInAreaTerminal controller, Collision2D collision)
    {
        
    }

    public override void StateOnTriggerExit2D(WaitInAreaTerminal controller, Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            controller.TransitionToState(controller.StateFactory.GetState(State.playerNotInRange));
        }
    }

    public override void StateUpdate(WaitInAreaTerminal controller)
    {
        float progress = controller.Progress / _timeToComplete; // What percentage of the time we've waited in the Terminal

        controller.ProgressBar.UpdateFill(progress);
        HandleStateTransitions(controller);
    }
    */
}
