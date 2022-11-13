using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Laser/MoveBy")]
    public class MoveBy : LaserCommand {
        #region Inspector
        [SerializeField]
        private float angle;
        public float Angle => angle;
        // public float Angle {get {
        //     if(this.toTransform != null)
        //         this.angle = GetSignedAngle(Vector2.up, (Vector2) this.toTransform.position - this.origin);   
        //     return this.angle;
        // }}
        [SerializeField]
        private float speed;
        public float Speed => speed;
        #endregion
        public static float GetSignedAngle(Vector2 p1, Vector2 p2)
        {
            return Mathf.Atan2(p1.y*p2.x - p1.x*p2.y, p1.x*p2.x+p1.y*p2.y) * Mathf.Rad2Deg;
        }
        // private Transform toTransform;
        // public Transform ToTransform => toTransform;

        // private Vector2 origin;
        // public Vector2 Origin => origin;

        // public override void Hydrate(AttackStep attackStep, GameObject caller)
        // {
        //     this.toTransform = attackStep.ToTransform;
        //     this.origin = caller.transform.position;
        // }
    }
}