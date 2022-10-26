using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Hazard Behaviors/Direct")]
public class Direct : BehaviorBase
{

    override public void HandleSequence(SequenceData data)
    {
        data.Caller.StartCoroutine(FireProjectiles(data));
    }

    private IEnumerator FireProjectiles(SequenceData data)
    {
        for(int i=0; i < data.ProjectileCount; ++i)
        {
            var obj = Instantiate(data.ProjectilePrefab);
            if(obj == null) continue;
            obj.SetActive(false);
            var to = (Vector2) data.To.position;
            var from = data.From;
            Rigidbody2D m_RigidBody = obj.GetComponent<Rigidbody2D>();
            var projectileScript = obj.GetComponent<HazardProjectile>();
            var angle = Mathf.Atan2(to.y - from.y, to.x - from.x);
            obj.transform.Rotate(new Vector3(0, 0, angle * Mathf.Rad2Deg));

            // obj.transform.rotation = Quaternion.FromToRotation(from.normalized, to.normalized);
            obj.transform.position = from;
            var commands = new Queue<HazardCommand>();
            commands.Enqueue(new FireCommand(from, data.To, data.Speed));
            obj.SetActive(true);
            if(obj == null) continue;
            projectileScript.ExecuteCommands(commands);
            yield return new WaitForSeconds(data.SecondsBetweenProjectiles);
        }
    }
   
}
