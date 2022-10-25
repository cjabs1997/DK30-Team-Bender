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

    // private void MoveToVector(GameObject obj, Vector2 to, float speed)
    // {
    //     while(Vector2.Distance(obj.transform.position, to) >= 0.001 )
    //         obj.transform.position = Vector2.MoveTowards(obj.transform.position, to, speed);
    //     // yield return new WaitUntil(() => {
    //     //     obj.transform.position = Vector2.MoveTowards(obj.transform.position, to, speed);
    //     //     return Vector2.Distance(obj.transform.position, to) <= 0.001f;
    //     // });
    // }

    private IEnumerator SetupProjectiles(SequenceData data)
    {
        for(int i = 0; i < data.Projectiles.Count; ++i)
        {
            var obj = data.Projectiles[i];
            if(obj == null) continue;
            var delta = (float)i/(float)(data.Projectiles.Count-1);
            var angle = Mathf.Lerp(0, ArcAngle, delta);
            var point = CalculateArcPoint(data.From, angle);
            obj.transform.position = point;
            // MoveToVector(obj, point, data.Speed);
            obj.SetActive(true);
            yield return new WaitUntil(() => Vector2.Distance(obj.transform.position, point) <= 0.001f);
            yield return new WaitForSeconds(data.SecondsBetweenProjectiles);
        }

        data.Caller.StartCoroutine(FireProjectiles( data));
        yield return null;
    }

    private IEnumerator FireProjectiles(SequenceData data)
    {   
        for(int i = 0; i < data.Projectiles.Count; ++i){
            var obj = data.Projectiles[i];
            if(obj == null) continue;
            var from = obj.transform.position;
            var to = data.To;
            Rigidbody2D m_RigidBody = obj.GetComponent<Rigidbody2D>();
            var angle = Mathf.Atan2(from.y - to.y, from.x - to.x);
            obj.transform.rotation = Quaternion.FromToRotation(from.normalized, toDirection: to.normalized);
            var force = CalculateVectorForce(from, data.To, data.Speed, m_RigidBody.mass);
            obj.SetActive(true);
            if(obj != null)
                m_RigidBody.AddForce(force);
            yield return new WaitForSeconds(data.SecondsBetweenProjectiles);
        }
    }
      override public void HandleSequence(SequenceData data)
      {
        data.Caller.StartCoroutine(SetupProjectiles(data));
        // Destroy(this);
      }
}
