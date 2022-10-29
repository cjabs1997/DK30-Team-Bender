using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazard Behaviors/Arc")]
public class ArcAttack : BehaviorBase
{

    [SerializeField] public float ArcRadius;
    [SerializeField, Range(0f, 130f)] float ArcAngle;
    [SerializeField] float SetupSpeedMultiplier;
    [SerializeField] bool ChaseTarget;
    [SerializeField] float ChaseTime;

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


            commands.Enqueue(new MoveCommand(data.From, point, data.Speed * SetupSpeedMultiplier));
            commands.Enqueue(new WaitCommand(data.SecondsBetweenProjectiles));
            commands.Enqueue(new WaitCommand(i * data.SecondsBetweenProjectiles));
            if(ChaseTarget){
                commands.Enqueue(new MoveCommand(point, data.To, data.Speed, stopAtDestination: false, timeLimit: this.ChaseTime));
            }
            else
            {
                commands.Enqueue(new FireCommand(point, data.To, data.Speed));
            }
            
            obj.SetActive(true);
            if(obj == null) continue;
            projectileScript.ExecuteCommands(commands);
            yield return null;
        }
    }

    override public void HandleSequence(SequenceData data)
    {
        data.Caller.StartCoroutine( SpawnProjectiles(data) );
    }
}
