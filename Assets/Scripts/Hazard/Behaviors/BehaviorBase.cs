using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SequenceData
{
    private Vector2 to;
    public Vector2 To => to;
    private Vector2 from;
    public Vector2 From => from;
    private MonoBehaviour caller;
    public MonoBehaviour Caller => caller;
    private float speed;
    public float Speed => speed;
    private float secondsBetweenProjectiles;
    public float SecondsBetweenProjectiles => secondsBetweenProjectiles;
    private List<GameObject> projectiles;
    public List<GameObject> Projectiles => projectiles;
    public SequenceData(Vector2 from, Vector2 to, MonoBehaviour caller, float speed, float secondsBetweenProjectiles, List<GameObject> projectiles)
    {
        this.from = from;
        this.to = to;
        this.caller = caller;
        this.speed = speed;
        this.secondsBetweenProjectiles = secondsBetweenProjectiles;
        this.projectiles = projectiles;
    }
}

abstract public class BehaviorBase : ScriptableObject
{
    abstract public void HandleSequence(SequenceData data);

}