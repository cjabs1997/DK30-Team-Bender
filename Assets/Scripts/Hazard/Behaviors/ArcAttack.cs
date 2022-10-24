using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazard Behaviors/Arc")]
public class ArcAttack : BehaviorBase
{

    [SerializeField] public float ArcRadius;
    [SerializeField, Range(0f, 130f)] float ArcAngle;
    
    private float secondsBetweenProjectiles = 0;
    private MonoBehaviour caller;

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

    private IEnumerator SetupProjectiles(Vector2 from, Vector2 to, float speed, List<GameObject> projectiles)
    {
        for(int i = 0; i < projectiles.Count; ++i)
        {
            var delta = (float)i/(float)(projectiles.Count-1);
            var angle = Mathf.Lerp(0, ArcAngle, delta);
            var point = CalculateArcPoint(from, angle);
            Debug.Log(point);
            var obj = projectiles[i];
            obj.transform.position = point;
            obj.SetActive(true);
            yield return new WaitForSeconds(this.secondsBetweenProjectiles);
        }

        caller.StartCoroutine(FireProjectiles(to, speed, projectiles ));
        yield return null;
    }

    private IEnumerator FireProjectiles(Vector2 to, float speed, List<GameObject> projectiles)
    {   
        for(int i = 0; i < projectiles.Count; ++i){
            var obj = projectiles[i];
            var from = obj.transform.position;
            Rigidbody2D m_RigidBody = obj.GetComponent<Rigidbody2D>();
            var angle = Mathf.Atan2(from.y - to.y, from.x - to.x);
            obj.transform.rotation = Quaternion.FromToRotation(from.normalized, to.normalized);
            var force = CalculateVectorForce(from, to, speed, m_RigidBody.mass);
            obj.SetActive(true);
            if(obj != null)
                m_RigidBody.AddForce(force);
            yield return new WaitForSeconds(secondsBetweenProjectiles);
        }
    }
      override public void HandleSequence(MonoBehaviour caller, Vector2 from, Vector2 to, float speed, float secondsBetweenProjectiles, List<GameObject> projectiles)
      {
        this.caller = caller;
        this.secondsBetweenProjectiles = secondsBetweenProjectiles;
        caller.StartCoroutine(SetupProjectiles(from, to, speed, projectiles));
        Destroy(this);
      }
}
