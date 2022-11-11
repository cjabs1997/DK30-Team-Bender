using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaserCommands;

[CreateAssetMenu(menuName="Hazards/Hazard Behaviors/Laser")]
public class LaserAttack : BehaviorBase
{   
    [SerializeField]
    private float startAngle;
    public float StartAngle => startAngle;
    private GameObject laserPrefab;
    private Queue<HazardCommand> commands;
    private HazardLaser laserScript;

    [SerializeField]
    private float startWaitTime;
    public float StartWaitTime => startWaitTime;
    [SerializeField]
    private float endWaitTime;
    public float endtWaitTime => endWaitTime;


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
        this.commands.Enqueue(new ToggleActivation(true, startAngle * Mathf.Rad2Deg));
        this.commands.Enqueue(new WaitCommand(startWaitTime));
        this.commands.Enqueue(new MoveBy(data.Caller.transform.position, data.To, data.Speed));
        this.commands.Enqueue(new WaitCommand(endWaitTime));
        this.commands.Enqueue(new ToggleActivation(false));
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
