using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazards/Attacks/Laser")]
public class LaserAttack : AttackCommands
{
    #region Inspector

    [SerializeField]
    private LaserCommand[] commands;
    public override HazardCommand[] Commands => commands;

    #endregion

}
