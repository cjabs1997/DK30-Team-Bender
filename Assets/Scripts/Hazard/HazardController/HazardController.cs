using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class HazardController : MonoBehaviour
{   
    #region Inspector
    [SerializeField]
    private bool cycles;
    public bool Cycles => cycles;
    [SerializeField]
    private int cyclesToFire;
    public int CyclesToFire => cyclesToFire;
    [SerializeField]
    private Sequence[] hazardSequences;
    public Sequence[] HazardSequences => hazardSequences;
    [SerializeField]
    private SimpleAudioEvent onFireSound;
    public SimpleAudioEvent OnFireSound => onFireSound;
    #endregion

    private int cyclesPassed = 0;
    private AudioSource audioSource;
    private Animator animator;
    public void CycleEnd()
    {
        if(!cycles) return;
        cyclesPassed++;
        if(cyclesPassed >= cyclesToFire)
        {
            cyclesPassed = 0;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Fire()
    {
        foreach(var sequence in this.hazardSequences)
        {
            StartCoroutine(this.StartSequence(sequence));
        }
    }

    public Queue<CommandContext> ProcessSequence(GameObject caller, Sequence sequence)
    {
        var commands = new Queue<CommandContext>();
        foreach(var sequenceStep in sequence.SequenceSteps)
        {
            foreach(var command in sequenceStep.Attack.Commands)
            {
                var commandContext = new CommandContext(command, caller.transform.position, sequenceStep.ToTransform, caller);
                commands.Enqueue(commandContext);
            }
        }
        return commands;
    }

    public IEnumerator StartSequence(Sequence sequence)
    {
        GameObject obj = GameObject.Instantiate(sequence.ProjectilePrefab, this.gameObject.transform);
        obj.SetActive(false);
        HazardObject script = obj.GetComponent<HazardObject>();
        var commandQueue = this.ProcessSequence(this.gameObject, sequence);
        script.ExecuteCommands(commandQueue);
        obj.SetActive(true);
        yield return null;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // this.Fire();
            if(this.OnFireSound != null && this.audioSource != null)
                this.OnFireSound.Play(this.audioSource);

            if(this.animator != null)
                this.animator.SetTrigger("OnFire");
            else
                this.Fire();
        }
    }

}
