using UnityEngine;

public abstract class HazardCommand : ScriptableObject {}
public abstract class ProjectileCommand : HazardCommand {}
public abstract class LaserCommand : HazardCommand {}
