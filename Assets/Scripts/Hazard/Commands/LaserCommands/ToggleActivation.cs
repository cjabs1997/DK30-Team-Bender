using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCommands {
    [CreateAssetMenu(menuName ="Hazards/Commands/Laser/Toggle Activation")]
    public class ToggleActivation : LaserCommand
    {
        #region Inspector
        [SerializeField]
        private bool activate;
        public bool Activate => activate;
        [SerializeField]
        private float startAngle;
        public float StartAngle => startAngle;
        #endregion
    }
}