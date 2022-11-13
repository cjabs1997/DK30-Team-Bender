using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Laser/Wait")]
    public class WaitCommand : LaserCommand
    {
        #region Inspector
        [SerializeField]
        private float seconds;
        public float Seconds => seconds;
        #endregion
    }
}