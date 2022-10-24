using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class BehaviorBase : ScriptableObject
{

    abstract public void HandleSequence(MonoBehaviour caller, Vector2 from, Vector2 to, float speed, float secondsBetweenProjectiles, List<GameObject> projectiles);

}