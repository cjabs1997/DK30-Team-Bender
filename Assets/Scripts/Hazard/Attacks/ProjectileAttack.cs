using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectileCommands;

[CreateAssetMenu(menuName="Hazards/Attacks/Projectile")]
public class ProjectileAttack : AttackCommands
{
    # region Inspector
    [SerializeField]
    private ProjectileCommand[] commands;
    public override HazardCommand[] Commands => commands;

    #endregion

}
