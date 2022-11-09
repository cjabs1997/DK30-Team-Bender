using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] private TerminalSet _terminalSet; // All terminals present in the scene
    [SerializeField] private TerminalSet _activatedTerminalSet; // Only terminals that the player has activated

    [SerializeField] private bool _orderMatters;

    public void TerminalActivated()
    {
        Debug.Log("Terminal Activated");

        if (_activatedTerminalSet.Value.Count >= _terminalSet.Value.Count)
        {
            Debug.Log("ALL TERMINALS ACTIVATED");
        }
    }
}
