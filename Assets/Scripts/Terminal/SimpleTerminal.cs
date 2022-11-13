using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTerminal : Terminal
{
    private SimpleTerminalController _controller;

    private void Awake()
    {
        _controller = this.GetComponent<SimpleTerminalController>();
    }

    public override void ActivateTerminal()
    {
        _controller.TransitionToState(_controller.StateFactory.GetState(States.State.playerInRange));
    }
}
