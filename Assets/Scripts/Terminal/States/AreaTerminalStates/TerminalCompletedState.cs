using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

[CreateAssetMenu(menuName = "States/Terminal/TerminalCompleted")]
public class TerminalCompletedState : AreaTerminalState
{
    public override State stateKey => State.terminalComplete;
    //[SerializeField] private SimpleAudioEvent _terminalActivatedAudio;

    public override void EnterState(WaitInAreaTerminalController controller)
    {
        controller.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        controller.TerminalProgressBar.gameObject.SetActive(false);
        controller.Terminal.activatedTerminalSet.AddValue(controller.Terminal);
        //_terminalActivatedAudio.Play(controller.AudioSource);
        controller.Terminal.TerminalCompleted.Raise();
    }
}
