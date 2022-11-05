using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[CreateAssetMenu(menuName = "States/Terminal/TerminalCompleted")]
public class TerminalCompletedState : AreaTerminalState
{
    public override State stateKey => State.terminalComplete;

    public override void EnterState(WaitInAreaTerminalController controller)
    {
        controller.TerminalProgressBar.gameObject.SetActive(false);
    }
}
