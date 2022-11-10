using UnityEngine;

public class Terminal : MonoBehaviour
{
    [Tooltip("Which event is fired when the terminal is activated/completed.")]
    [SerializeField] private GameEvent _terminalCompleted;
    public GameEvent TerminalCompleted => _terminalCompleted;

    [Tooltip("Which order in the sequence the terminal is. Ignore if not caring about a sequence. I don't do any checking if several terminals have the same value :)")]
    [Range(1, 10)]
    [SerializeField] private int _order;
    public int Order => _order;


    public TerminalSet terminalSet; // Runtime set of all terminals in the scene
    public TerminalSet activatedTerminalSet; // Runtime set of all terminals that have been activated in the scene




    private void OnEnable()
    {
        terminalSet.AddValue(this);
    }

    private void OnDisable()
    {
        terminalSet.RemoveValue(this);
        activatedTerminalSet.RemoveValue(this);
    }
}
