using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    [SerializeField] private TerminalSet _terminalSet; // All terminals present in the scene
    [SerializeField] private TerminalSet _activatedTerminalSet; // Only terminals that the player has activated

    [SerializeField] private GameEvent ResetTerminals;

    [SerializeField] private bool _orderMatters;
    private List<Terminal> _correctSequence;

    private void Start()
    {
        if(_orderMatters)
        {
            _correctSequence = GenerateSequence();
        }
    }


    public void TerminalActivated()
    {
        if (_activatedTerminalSet.Value.Count >= _terminalSet.Value.Count)
        {
            if(_orderMatters)
            {
                if(CheckSequence())
                {
                    Debug.Log("ALL TERMINALS ACTIVATED CORRECTLY");
                }
                else
                {
                    Debug.Log("WRONG SEQUENCE RESETTING");
                    _activatedTerminalSet.ResetSet();
                    ResetTerminals.Raise();
                }
                return;
            }

            Debug.Log("ALL TERMINALS ACTIVATED");
        }
    }

    private List<Terminal> GenerateSequence()
    {
        return _terminalSet.Value.OrderBy(t => t.Order).ToList();
    }

    // Not sure we want this functionality but putting it here for now
    public bool CheckTerminal()
    {
        if(_terminalSet.Value.Last() == _correctSequence[_activatedTerminalSet.Value.Count-1])
        {
            return true;
        }

        return false;
    }

    private bool CheckSequence()
    {
        for (int i = 0; i < _terminalSet.Value.Count; i++)
        {
            if (_correctSequence[i] != _activatedTerminalSet.Value[i])
            {
                return false;
            }
        }

        return true;
    }
}
