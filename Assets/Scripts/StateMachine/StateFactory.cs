using System.Collections.Generic;
using States;
using UnityEngine;

public class StateNotFoundException : System.Exception { }

public abstract class StateFactory<T> : ScriptableObject
{
    protected Dictionary<State, T> States = new Dictionary<State, T>();

    [SerializeField] protected T[] stateList;


    public T GetState(State state)
    {
        return States.TryGetValue(state, out T s) ? s : throw new StateNotFoundException();
    }

    private void OnEnable()
    {
        PopulateStates();
    }

    protected abstract void PopulateStates();
}