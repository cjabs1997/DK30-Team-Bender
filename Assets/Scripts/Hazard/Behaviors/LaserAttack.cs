using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Hazard Behaviors/Laser")]
public class LaserAttack : BehaviorBase
{   
    [SerializeField]
    private float startAngle;
    public float StartAngle => startAngle;
    private GameObject laserPrefab;
    private Queue<HazardCommand> commands;
    private HazardLaser laserScript;

    private void initialize(SequenceData data)
    {
        if(laserPrefab == null)
            this.laserPrefab = Instantiate<GameObject>(data.ProjectilePrefab, data.Caller.transform);
        laserPrefab.SetActive(false);
        this.commands = new Queue<HazardCommand>();
        laserScript = laserPrefab.GetComponent<HazardLaser>();
    }

    private IEnumerator StartLaser(SequenceData data)
    {
        var startPos = new Vector2(Mathf.Sin(startAngle * Mathf.Deg2Rad), Mathf.Cos(startAngle* Mathf.Deg2Rad));
        this.commands.Enqueue(new FireCommand(data.From, startPos, 1));
        this.commands.Enqueue(new MoveCommand(data.From, data.To, data.Speed));
        this.commands.Enqueue(new FireCommand(data.From, data.To, 0));
        laserPrefab.SetActive(true);
        laserScript.ExecuteCommands(this.commands);
        yield return null;
    }

    override public void HandleSequence(SequenceData data)
    {
        this.initialize(data);
        data.Caller.StartCoroutine(StartLaser(data));
        this.laserPrefab = null;
    }

}
