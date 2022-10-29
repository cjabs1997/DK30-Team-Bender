using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazard Behaviors/Arc")]
public class ArcAttack : BehaviorBase
{

    [SerializeField] public float ArcRadius;
    [SerializeField, Range(0f, 130f)] float ArcAngle;
    [SerializeField] float SetupSpeedMultiplier;

    private Vector2 CalculateArcPoint(Vector2 origin, float angle)
    {
        // C+r(cos(θ),sin(θ))
        var adjustedAngleRadians = Mathf.Deg2Rad * (angle + 90 - ArcAngle/2);
        return origin + ArcRadius * new Vector2(Mathf.Cos(adjustedAngleRadians), Mathf.Sin(adjustedAngleRadians));
    }
    
    private IEnumerator SpawnProjectiles(SequenceData data)
    {
        // var delta = 1.0f/data.ProjectileCount;
        for(int i=0; i < data.ProjectileCount; ++i)
        {
            var obj = Instantiate(data.ProjectilePrefab);
            obj.SetActive(false);
            var delta = (float)i/(data.ProjectileCount-1);
            if(delta.Equals(float.NaN)) delta = 0.5f;
            var angle = Mathf.Lerp(0, ArcAngle, delta);
            var point = CalculateArcPoint(data.From, angle);
            var projectileScript = obj.GetComponent<HazardProjectile>();

            var commands = new Queue<HazardCommand>();
            var pos1 = Vector2.MoveTowards(data.From, point, Vector2.Distance(data.From, point)/2);
            var pos2 = Vector2.MoveTowards(pos1, point, Vector2.Distance(pos1, point)/2);
            var pos3 = Vector2.MoveTowards(pos2, point, Vector2.Distance(pos2, point)/2);
            var pos4 = Vector2.MoveTowards(pos3, point, Vector2.Distance(pos3, point));


            // commands.Enqueue(new MoveCommand(data.From, point, data.Speed * SetupSpeedMultiplier));
            commands.Enqueue(new MoveCommand(data.From, pos1, data.Speed * SetupSpeedMultiplier));
            commands.Enqueue(new MoveCommand(data.From, pos2, data.Speed * SetupSpeedMultiplier * 0.5f));
            commands.Enqueue(new MoveCommand(data.From, pos3, data.Speed * SetupSpeedMultiplier * 0.25f));
            commands.Enqueue(new MoveCommand(data.From, pos4, data.Speed * SetupSpeedMultiplier * 0.125f));

            commands.Enqueue(new WaitCommand(data.SecondsBetweenProjectiles));
            commands.Enqueue(new WaitCommand(i * data.SecondsBetweenProjectiles));

            commands.Enqueue(new FireCommand(point, data.To, data.Speed));
            
            obj.SetActive(true);
            if(obj == null) continue;
            projectileScript.ExecuteCommands(commands);
            yield return null;
        }
    }

    override public void HandleSequence(SequenceData data)
    {
        // data.Caller.StartCoroutine(SetupProjectiles(data));
        data.Caller.StartCoroutine( SpawnProjectiles(data) );
    }
}
