using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInAreaTerminal : StateController
{
    public float Progress { get; private set; }
    [SerializeField] TerminalProgressBar _progressBar;
    public TerminalProgressBar ProgressBar { get { return _progressBar; } }


}
