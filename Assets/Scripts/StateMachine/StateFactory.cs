using System.Collections.Generic;
using States;
using UnityEngine;

public class StateNotFoundException : System.Exception { }

[CreateAssetMenu]
public class StateFactory : ScriptableObject
{
    private Dictionary<State, BaseState> States = new Dictionary<State, BaseState>();

    [SerializeField] private BaseState[] stateList;


    public BaseState GetState(State state)
    {
        return States.TryGetValue(state, out BaseState s) ? s : throw new StateNotFoundException();
    }

    private void OnEnable()
    {
        PopulateStates();
    }

    private void PopulateStates()
    {
        if (States.Count != 0) return;

        foreach (BaseState s in stateList)
        {
            States[s.stateKey] = s;
        }
    }
}