using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredButton : ScriptableObject
{
    [SerializeField] private Color _buttonColor;
    public Color ButtonColor => _buttonColor;
}
