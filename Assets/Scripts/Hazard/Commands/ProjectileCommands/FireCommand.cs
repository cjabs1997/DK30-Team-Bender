using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Projectile/Fire")]
    public class FireCommand : ProjectileCommand
    {
        #region Inspector  
        [SerializeField]
        private float speed;
        public float Speed => speed;
        #endregion
        // private Vector2 from;
        // public Vector2 From => from;
        // private Transform toTransform;
        // public Transform ToTransforms => toTransform;
        // private Vector2 to;
        // public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position :  to ;} }
        // public override void Hydrate(AttackStep sequence, GameObject caller)
        // {
        //     this.from = caller.transform.position;
        //     this.toTransform = sequence.ToTransform;
        //     this.to = sequence.To;
        // }

    }
}