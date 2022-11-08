using UnityEngine;
using System;
using States;

public abstract class BaseState<T> : ScriptableObject
{
    public abstract State stateKey { get; }

    public virtual void EnterState(T controller)
    {
        
    }

    public virtual void StateUpdate(T controller)
    {

    }

    public virtual void StateFixedUpdate(T controller)
    {

    }

    public virtual void HandleStateTransitions(T controller)
    {

    }

    public virtual void StateOnCollisionEnter2D(T controller, Collision2D collision)
    {

    }

    public virtual void StateOnCollisionExit2D(T controller, Collision2D collision)
    {

    }

    public virtual void StateOnTriggerEnter2D(T controller, Collider2D collision)
    {

    }

    public virtual void StateOnTriggerExit2D(T controller, Collider2D collision)
    {

    }

    public virtual void ExitState(T controller)
    {

    }
}
