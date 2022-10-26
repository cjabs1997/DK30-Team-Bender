using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazard Behaviors/Arc")]
public class ArcAttack : BehaviorBase
{

    [SerializeField] public float ArcRadius;
    [SerializeField, Range(0f, 130f)] float ArcAngle;

    private Vector2 CalculateVectorForce(Vector2 from, Vector2 to, float speed, float mass)
    {
        Vector2 direction = (to - from).normalized;
        return mass * speed * direction;
    }

    private Vector2 CalculateArcPoint(Vector2 origin, float angle)
    {
        // C+r(cos(θ),sin(θ))
        var adjustedAngleRadians = Mathf.Deg2Rad * (angle + 90 - ArcAngle/2);
        return origin + ArcRadius * new Vector2(Mathf.Cos(adjustedAngleRadians), Mathf.Sin(adjustedAngleRadians));
    }

    private IEnumerator SpawnProjectiles(SequenceData data)
    {
        for(int i=0; i < data.ProjectileCount; ++i)
        {
            var obj = Instantiate(data.ProjectilePrefab);
            obj.SetActive(false);
            var delta = (float)i/(float)(data.ProjectileCount-1);
            var angle = Mathf.Lerp(0, ArcAngle, delta);
            var point = CalculateArcPoint(data.From, angle);
            obj.transform.position = point;
            var projectileScript = obj.GetComponent<HazardProjectile>();
            var commands = new Queue<HazardCommand>();
            commands.Enqueue(new WaitCommand(data.SecondsBetweenProjectiles * data.ProjectileCount));
            commands.Enqueue(new FireCommand(point, data.To, data.Speed));
            obj.SetActive(true);
            if(obj == null) continue;
            projectileScript.ExecuteCommands(commands);
            yield return new WaitForSeconds(data.SecondsBetweenProjectiles);
        }
    }

    override public void HandleSequence(SequenceData data)
    {
        // data.Caller.StartCoroutine(SetupProjectiles(data));
        data.Caller.StartCoroutine( SpawnProjectiles(data) );
    }
}
