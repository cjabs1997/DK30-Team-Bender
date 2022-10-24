using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Hazard Behaviors/Direct")]
public class Direct : BehaviorBase
{  
    private float secondsBetweenProjectiles = 0;
    
    private Vector2 CalculateVectorForce(Vector2 from, Vector2 to, float speed, float mass)
    {
        Vector2 direction = (to - from).normalized;
        return mass * speed * direction;
    }

    override public void HandleSequence(MonoBehaviour caller, Vector2 from, Vector2 to, float speed, float secondsBetweenProjectiles, List<GameObject> projectiles)
    {
        this.secondsBetweenProjectiles = secondsBetweenProjectiles;
        caller.StartCoroutine(FireProjectiles(from, to, speed, projectiles));
        Destroy(this);
    }

    private IEnumerator FireProjectiles(Vector2 from, Vector2 to, float speed, List<GameObject> projectiles)
    {   
        for(int i = 0; i < projectiles.Count; ++i){
            var obj = projectiles[i];
            Rigidbody2D m_RigidBody = obj.GetComponent<Rigidbody2D>();
            var angle = Mathf.Atan2(from.y - to.y, from.x - to.x);
            obj.transform.rotation = Quaternion.FromToRotation(from.normalized, to.normalized);
            obj.transform.position = from;
            var force = CalculateVectorForce(from, to, speed, m_RigidBody.mass);
            obj.SetActive(true);
            if(obj != null)
                m_RigidBody.AddForce(force);
            yield return new WaitForSeconds(secondsBetweenProjectiles);
        }
    }
   
}
