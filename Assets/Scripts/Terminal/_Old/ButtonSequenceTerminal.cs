using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequenceTerminal : MonoBehaviour
{
    public Terminal Terminal { get; set; }

    [SerializeField] ColoredButton[] _possibleButtons;
    [SerializeField] List<UnityEngine.UI.Image> _sequenceImages;

    private ColoredButton[] _correctSequence;
    private ColoredButton[] _enteredSequence;

    private List<UnityEngine.UI.Button> _buttons;
    private int _buttonsPressed;
    private int _totalButtons;

    private void Awake()
    {
        _buttons = new List<UnityEngine.UI.Button>(this.GetComponentsInChildren<UnityEngine.UI.Button>());
        _totalButtons = _buttons.Count;

        _enteredSequence = new ColoredButton[_totalButtons];
        _correctSequence = new ColoredButton[_totalButtons];
        GenerateSequence();
        InitSequenceImages();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }
    }

    public void EnterButton(ColoredButton value)
    {
        _enteredSequence[_buttonsPressed] = value;
        _buttonsPressed++;

        if (_buttonsPressed == _totalButtons)
            CheckSequence();

        return;
    }

    private void GenerateSequence()
    {
        for(int i = 0; i < _totalButtons; i++)
        {
            _correctSequence[i] = _possibleButtons[i];
        }

        int n = _totalButtons;
        System.Random random = new System.Random();
        while(n > 1)
        {
            int k = random.Next(n--);
            ColoredButton temp = _correctSequence[n];
            _correctSequence[n] = _correctSequence[k];
            _correctSequence[k] = temp;
        }
    }

    private void CheckSequence()
    {
        for (int i = 0; i < _totalButtons; i++)
        {
            if(_correctSequence[i].ButtonColor != _enteredSequence[i].ButtonColor)
            {
                ResetButtons();
                Debug.Log("FAILURE");
                return;
            }
        }

        Debug.Log("VICTORY");
        //this.Terminal.TerminalExited();
        Destroy(this.gameObject);
    }

    private void InitSequenceImages()
    {
        for (int i = 0; i < _totalButtons; i++)
        {
            _sequenceImages[i].color = _correctSequence[i].ButtonColor;
        }
    }

    private void ResetButtons()
    {
        _buttonsPressed = 0;

        foreach(UnityEngine.UI.Button b in _buttons)
        {
            b.interactable = true;
        }
    }
}
