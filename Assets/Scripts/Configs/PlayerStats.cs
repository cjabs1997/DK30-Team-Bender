using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [Header("Jumping and Falling")]
    [Tooltip("Maximum speed under the effects of gravity.")]
    [SerializeField] float _terminalVelocity;
    public float TerminalVelocity => _terminalVelocity;

    [Tooltip("How much force for the intial jump")]
    [SerializeField] float _initialJump;
    public float InitialJump => _initialJump;

    [Tooltip("How long until the effects of gravity take place for a jump")]
    [SerializeField] float _maxJumpTime;
    public float MaxJumpTime => _maxJumpTime;

    [Tooltip("The force of gravity applied when the object is falling")]
    [SerializeField] float _gravity;
    public float Gravity => _gravity;

    [Tooltip("")]
    [SerializeField] private float _maxHorizontalForceInAir;
    public float MaxHorizontalForceInAir => _maxHorizontalForceInAir;

    [Tooltip("What's considered ground")]
    [SerializeField] private ContactFilter2D _groundMask;
    public ContactFilter2D GroundMask => _groundMask;

    [Tooltip("How long after the player falls can they still jump")]
    [SerializeField] private float _jumpDelay;
    public float JumpDelay => _jumpDelay;


    [Header("General Movement")]
    [Tooltip("Max speed the player can achieve")]
    [SerializeField] private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;

    [Tooltip("The max amount of force that can be applied on the object when they're grounded for lateral movement")]
    [SerializeField] private float _maxHorizontalGroundedForce;
    public float MaxHorizontalGroudedForce => _maxHorizontalGroundedForce;

    [Tooltip("How long until the player should stop when they release inputs")]
    [SerializeField] private float _stopDistance;
    public float StopDistance => _stopDistance;

}
