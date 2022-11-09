using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInAreaTerminalController : StateController<AreaTerminalState, WaitInTerminalStateFactory>
{
    public float Progress { get; set; }
    [SerializeField] private TerminalProgressBar _terminalProgressBar;
    public TerminalProgressBar TerminalProgressBar => _terminalProgressBar;
    [SerializeField] private float _timeToComplete; // Should put this behind a stat I think
    public float TimeToComplete => _timeToComplete;

    public TerminalSet terminalSet;
    public TerminalSet activatedTerminalSet;
    [SerializeField] private GameEvent _terminalCompleted;
    public GameEvent TerminalCompleted => _terminalCompleted;


    private void Start()
    {
        currentState.EnterState(this);
        Progress = 0;
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

    private void OnEnable()
    {
        terminalSet.AddValue(this);
    }

    private void OnDisable()
    {
        terminalSet.RemoveValue(this);
        activatedTerminalSet.RemoveValue(this);
    }
}
