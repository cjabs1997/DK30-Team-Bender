using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Hazard Behaviors/Direct")]
public class Direct : BehaviorBase
{  
    private Vector2 CalculateVectorForce(Vector2 from, Vector2 to, float speed, float mass)
    {
        Vector2 direction = (to - from).normalized;
        return mass * speed * direction;
    }

    override public void HandleSequence(SequenceData data)
    {
        data.Caller.StartCoroutine(FireProjectiles(data));
        // Destroy(this);
    }

    private IEnumerator FireProjectiles(SequenceData data)
    {   
        for(int i = 0; i < data.Projectiles.Count; ++i){
            var obj = data.Projectiles[i];
            if(obj == null) continue;
            var to = data.To;
            var from = data.From;
            var speed = data.Speed;
            Rigidbody2D m_RigidBody = obj.GetComponent<Rigidbody2D>();
            var angle = Mathf.Atan2(from.y - to.y, from.x - to.x);
            obj.transform.rotation = Quaternion.FromToRotation(from.normalized, to.normalized);
            obj.transform.position = from;
            var force = CalculateVectorForce(from, to, speed, m_RigidBody.mass);
            obj.SetActive(true);
            if(obj != null)
                m_RigidBody.AddForce(force);
            yield return new WaitForSeconds(data.SecondsBetweenProjectiles);
        }
    }
   
}
