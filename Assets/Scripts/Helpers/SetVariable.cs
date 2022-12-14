// Based on the architecture described by Ryan Hipple in his Unite 2017 talk
//
//https://www.youtube.com/watch?v=raQ3iHhE_Kk
//https://github.com/roboryantron/Unite2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVariable<T> : ScriptableObject
{
    public List<T> Value = new List<T>();


    public void AddValue(T val)
    {
        if (!Value.Contains(val))
        {
            Value.Add(val);
        }
    }

    public void RemoveValue(T val)
    {
        if (Value.Contains(val))
        {
            Value.Remove(val);
        }
    }

    public void ResetSet()
    {
        Value.Clear();
    }
}
