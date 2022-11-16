using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInAreaTerminalController : StateController<AreaTerminalState, WaitInTerminalStateFactory>
{
    
    [SerializeField] private TerminalProgressBar _terminalProgressBar;
    public TerminalProgressBar TerminalProgressBar => _terminalProgressBar;

    [SerializeField] private SpriteRenderer _terminalSprite;
    public SpriteRenderer TerminalSprite => _terminalSprite;


    public WaitInAreaTerminal Terminal { get; private set; }
    public float Progress { get; set; }

    private bool _inRange; // This is gross but it trivializes some shit a ton

    protected override void Awake()
    {
        Terminal = this.GetComponentInParent<WaitInAreaTerminal>();
        AudioSource = this.GetComponentInParent<AudioSource>();
        _inRange = false;
    }

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
        if (collision.gameObject.CompareTag("Player"))
            _inRange = true;

        currentState.StateOnTriggerEnter2D(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _inRange = false;

        currentState.StateOnTriggerExit2D(this, collision);
    }

    public override void TransitionToState(AreaTerminalState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void ResetTerminal()
    {
        this.Progress = 0;
        this.TerminalProgressBar.UpdateFill(Progress);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        if (_inRange)
            this.TransitionToState(this.StateFactory.GetState(States.State.playerInRange));
        else
            this.TransitionToState(this.StateFactory.GetState(States.State.playerOutOfRange));
    }
}
