using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName="Hazards/Hazard")]
public class Hazard : ScriptableObject
{   
    [SerializeField] private GameObject projectilePrefab;
    public GameObject ProjectilePrefab => projectilePrefab;
    [SerializeField] private float speed;
    public float Speed => speed;
    [SerializeField] private int unitCount;
    public int UnitCount => unitCount;
    [SerializeField] private float secondsBetweenProjectiles;
    public float SecondsBetweenProjectiles => secondsBetweenProjectiles;
    [SerializeField] private BehaviorBase behavior;
    public BehaviorBase Behavior => behavior;

    public void StartSequence(Vector2 from, Transform to, MonoBehaviour caller)
    {
        var data = new SequenceData(from, to, caller, speed, secondsBetweenProjectiles, unitCount, projectilePrefab);
        behavior.HandleSequence(data);
    }

}
