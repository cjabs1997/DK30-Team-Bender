using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateFactory/WaitInAreaTerminal")]
public class WaitInTerminalStateFactory : StateFactory<AreaTerminalState>
{
    protected override void PopulateStates()
    {
        if (States.Count != 0) return;

        foreach (AreaTerminalState s in stateList)
        {
            States[s.stateKey] = s;
        }
    }
}
