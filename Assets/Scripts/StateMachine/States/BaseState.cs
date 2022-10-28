using UnityEngine;
using System;
using States;

public abstract class BaseState : ScriptableObject
{
    protected StateController controller;
    public abstract State stateKey { get; }

    public virtual void EnterState(StateController controller)
    {
        this.controller = controller;
    }


    public abstract void StateUpdate();

    public abstract void StateFixedUpdate();

    public abstract void HandleStateTransitions();

    public abstract void StateOnCollisionEnter2D(Collision2D collision);

    public abstract void ExitState();
}
