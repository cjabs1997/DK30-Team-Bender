using System.Collections;
using System.Collections.Generic;
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

    private Dictionary<System.Guid, List<GameObject>> sequences;

    public void StartSequence(MonoBehaviour caller, Vector2 from, Vector2 to)
    {
        var objects = new List<GameObject>();
        for(int i = 0; i < unitCount; ++i){
            var obj = Instantiate(projectilePrefab);
            objects.Add(obj);
            obj.SetActive(false);
        }
        var clone = Instantiate(behavior);
        clone.HandleSequence(caller, from, to, speed, secondsBetweenProjectiles, objects);
    }

}
