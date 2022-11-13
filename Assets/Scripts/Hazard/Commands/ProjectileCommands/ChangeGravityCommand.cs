using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Projectile/Change Gravity")]
    public class ChangeGravityCommand : ProjectileCommand
    {
        #region Inspector
        [SerializeField]
        private float gravity;
        public float Gravity => gravity;
        #endregion

    }
}