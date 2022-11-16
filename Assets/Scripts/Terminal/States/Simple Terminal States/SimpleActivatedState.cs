using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="States/SimpleTerminal/Activated")]
public class SimpleActivatedState : SimpleTerminalState
{
    public override States.State stateKey => States.State.playerInRange; // Bad name, didn't feel like adding a new one :P
    //[SerializeField] private SimpleAudioEvent _terminalActivatedAudio;

    public override void EnterState(SimpleTerminalController controller)
    {
        controller.SpriteRenderer.color = Color.green;

        controller.Terminal.activatedTerminalSet.AddValue(controller.Terminal);
        controller.Terminal.TerminalCompleted.Raise();
        //_terminalActivatedAudio.Play(controller.AudioSource);
    }
}
