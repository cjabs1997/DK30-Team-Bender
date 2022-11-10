using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[CreateAssetMenu(menuName = "States/Terminal/TerminalCompleted")]
public class TerminalCompletedState : AreaTerminalState
{
    public override State stateKey => State.terminalComplete;
    [SerializeField] private SimpleAudioEvent _terminalActivatedAudio;

    public override void EnterState(WaitInAreaTerminalController controller)
    {
        controller.TerminalProgressBar.gameObject.SetActive(false);
        controller.Terminal.activatedTerminalSet.AddValue(controller.Terminal);
        controller.Terminal.TerminalCompleted.Raise();
        _terminalActivatedAudio.Play(controller.AudioSource);

        // Do something to signify the terminal is off, turn off hitbox for instance :)
    }
}
