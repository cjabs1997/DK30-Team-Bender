using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Projectile/Move")]
    public class MoveCommand : ProjectileCommand 
    {  
        #region Inspector
        [SerializeField]
        private float speed;
        public float Speed => speed;
        [SerializeField] 
        private bool chaseTarget;
        public bool ChaseTarget => chaseTarget;
        [SerializeField] 
        private float chaseTimelimit;
        public float ChaseTimelimit => chaseTimelimit;
        [SerializeField] 
        private bool slowArrival;
        public bool SlowArrival => slowArrival;
        [SerializeField] 
        private float slowArrivalRadius;
        public float SlowArrivalRadius => slowArrivalRadius;

        [SerializeField]
        private float timeLimit;
        public float TimeLimit => timeLimit;
        #endregion

        // private Vector2 from;
        // public Vector2 From => from;
        // private Transform toTransform;
        // public Transform ToTransform => toTransform;
        // private Vector2 to;
        // public Vector2 To { get { return toTransform != null ? (Vector2) toTransform.position : to ;} }

        // public override void Hydrate(AttackStep sequence, GameObject caller)
        // {
        //     this.from = caller.transform.position;
        //     this.toTransform = sequence.ToTransform;
        //     this.to = sequence.To;
        // }

    }
}