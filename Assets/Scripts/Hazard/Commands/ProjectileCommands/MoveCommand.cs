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
        [SerializeField]
        private float maxSteeringForce;
        public float MaxSteeringForce => maxSteeringForce;
        #endregion

    }
}