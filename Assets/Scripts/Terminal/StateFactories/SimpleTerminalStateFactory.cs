using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateFactory/SimpleTerminal")]
public class SimpleTerminalStateFactory : StateFactory<SimpleTerminalState>
{
    protected override void PopulateStates()
    {
        if (States.Count != 0) return;

        foreach (SimpleTerminalState s in stateList)
        {
            States[s.stateKey] = s;
        }
    }
}
