using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Projectile/Change Drag")]
    public class ChangeDragCommand : ProjectileCommand
    {
        #region Inspector
        [SerializeField]
        private float linearDrag;
        public float LinearDrag => linearDrag;
        [SerializeField]
        private float angularDrag;
        public float AngularDrag => angularDrag;
        #endregion

    }
}