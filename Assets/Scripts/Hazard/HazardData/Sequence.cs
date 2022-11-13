using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sequence
{
    #region Inspector
    [SerializeField]
    private SequenceStep[] sequenceSteps;
    public SequenceStep[] SequenceSteps => sequenceSteps;
    [SerializeField]
    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab => projectilePrefab;

    #endregion

}

[System.Serializable]
public class SequenceStep
{
    #region Inspector
    [SerializeField]
    private AttackCommands attack;
    public AttackCommands Attack => attack;
    [SerializeField]
    private Transform toTransform;
    public Transform ToTransform => toTransform;

    #endregion
    private Vector2 to;
    public Vector2 To { 
        get {
            if(this.toTransform != null)
                return this.toTransform.position;
            return to;
        }
        set {
            this.to = value;
        }
    }

    public SequenceStep(SequenceStep og)
    {
        this.attack = og.Attack;
        this.toTransform = og.ToTransform;
        this.to = og.To;
    }

}