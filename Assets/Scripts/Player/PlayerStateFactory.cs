using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateFactory/PlayerStateFactory")]
public class PlayerStateFactory : StateFactory<PlayerState>
{
    protected override void PopulateStates()
    {
        if (States.Count != 0) return;

        foreach (PlayerState s in stateList)
        {
            States[s.stateKey] = s;
        }
    }
}
