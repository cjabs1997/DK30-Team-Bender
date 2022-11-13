using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTerminalController : StateController<SimpleTerminalState, SimpleTerminalStateFactory>
{
    public SimpleTerminal Terminal { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private bool _inRange; // This is gross but it trivializes some shit a ton

    protected override void Awake()
    {
        base.Awake();

        Terminal = this.GetComponent<SimpleTerminal>();
        SpriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        currentState.EnterState(this);
    }

    public override void TransitionToState(SimpleTerminalState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.StateOnTriggerEnter2D(this, collision);

        if (collision.gameObject.CompareTag("Player"))
            _inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.StateOnTriggerExit2D(this, collision);

        if (collision.gameObject.CompareTag("Player"))
            _inRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision) // This is gross but it fixes a bug sooooo...
    {
        if (collision.gameObject.CompareTag("Player"))
            _inRange = true;
    }

    public void ResetTerminal()
    {
        this.TransitionToState(this.StateFactory.GetState(States.State.playerOutOfRange));
    }
}
