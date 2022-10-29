using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is going to be some sloppy scripting but it should work fine, I'll make it better later
public class Terminal : MonoBehaviour
{
    [SerializeField] Transform _canvas;
    [SerializeField] GameObject _terminalPrefab;
    [SerializeField] StateController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerController.currentTerminal = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerController.currentTerminal = null;
    }

    public void OpenTerminal()
    {
        Instantiate(_terminalPrefab, _canvas);
    }
}
