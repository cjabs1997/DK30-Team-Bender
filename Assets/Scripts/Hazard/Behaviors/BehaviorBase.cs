using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SequenceData
{
    private Transform to;
    public Transform To => to;
    private Vector2 from;
    public Vector2 From => from;
    private MonoBehaviour caller;
    public MonoBehaviour Caller => caller;
    private float speed;
    public float Speed => speed;
    private float secondsBetweenProjectiles;
    public float SecondsBetweenProjectiles => secondsBetweenProjectiles;

    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab => projectilePrefab;
    private int projectileCount;
    public int ProjectileCount => projectileCount;
    public SequenceData(Vector2 from, Transform to, MonoBehaviour caller, float speed, float secondsBetweenProjectiles, int projectileCount, GameObject projectilePrefab)
    {
        this.from = from;
        this.to = to;
        this.caller = caller;
        this.speed = speed;
        this.secondsBetweenProjectiles = secondsBetweenProjectiles;
        this.projectileCount = projectileCount;
        this.projectilePrefab = projectilePrefab;
    }
}

abstract public class BehaviorBase : ScriptableObject
{
    abstract public void HandleSequence(SequenceData data);

}