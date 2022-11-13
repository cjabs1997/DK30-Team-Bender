using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Projectile/Wait")]
    public class WaitCommand : ProjectileCommand
    {
        #region Inspector
        [SerializeField]
        private float seconds;
        public float Seconds => seconds;
        #endregion

    }
}
