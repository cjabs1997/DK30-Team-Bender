using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInAreaTerminalController : StateController<AreaTerminalState, WaitInTerminalStateFactory>
{
    public float Progress { get; set; }
    [SerializeField] private TerminalProgressBar _terminalProgressBar;
    public TerminalProgressBar TerminalProgressBar { get { return _terminalProgressBar; } }
    [SerializeField] private float _timeToComplete; // Should put this behind a stat I think
    public float TimeToComplete { get { return _timeToComplete; } }

    public void Awake()
    {
        Progress = 0;
    }

    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.StateOnTriggerEnter2D(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.StateOnTriggerExit2D(this, collision);
    }

    public override void TransitionToState(AreaTerminalState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
