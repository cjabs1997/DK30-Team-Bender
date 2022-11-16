using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class HazardController : MonoBehaviour
{   
    #region Inspector
    [SerializeField]
    private float delayBetweenFire;
    public float DelayBetweenFire => delayBetweenFire;
    [SerializeField]
    private bool loop;
    public bool Loop => loop;
    [SerializeField]
    private bool startFiring;
    public bool StartFiring => startFiring;
    [SerializeField]
    private SimpleAudioEvent onFireSound;
    public SimpleAudioEvent OnFireSound => onFireSound;
    [SerializeField]
    private Sequence[] hazardSequences;
    public Sequence[] HazardSequences => hazardSequences;
    [SerializeField]
    private bool test;
    public bool Test => test;
    #endregion

    private bool firing;
    public bool Firing { get; set; }
    private bool canFire;
    private float lastFireTime;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        this.canFire = true;
        this.lastFireTime = Time.realtimeSinceStartup - this.delayBetweenFire;
        this.firing = this.startFiring;
    }

    public void ToggleFiringOff()
    {
        this.firing = false;
        // this.lastFireTime = Time.realtimeSinceStartup - this.delayBetweenFire;
    }

    public void ToggleFiringOn()
    {
        this.firing = true;
    }

    public void ToggleLaserOff()
    {
        HazardLaser[] lasers = this.GetComponentsInChildren<HazardLaser>();
        if(lasers.Length > 0)
        {
            foreach(var laser in lasers)
            {
                Destroy(laser.gameObject);
            }
        }
    }

    public void PreFire()
    {
        if(!this.canFire) return;
        this.canFire = false;
        this.lastFireTime = Time.realtimeSinceStartup;

        if(this.OnFireSound != null && this.audioSource != null)
            this.OnFireSound.Play(this.audioSource);

        if(this.animator != null)
            this.animator.SetTrigger("OnFire");
        else
            this.Fire();
    }

    public void Fire()
    {
        foreach(var sequence in this.hazardSequences)
        {
            StartCoroutine(this.StartSequence(sequence));
        }
    }

    private Queue<CommandContext> ProcessSequence(GameObject caller, Sequence sequence)
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

    private IEnumerator StartSequence(Sequence sequence)
    {
        GameObject obj = GameObject.Instantiate(sequence.ProjectilePrefab, this.gameObject.transform);
        obj.SetActive(false);
        HazardObject script = obj.GetComponent<HazardObject>();
        var commandQueue = this.ProcessSequence(this.gameObject, sequence);
        script.ExecuteCommands(commandQueue);
        obj.SetActive(true);
        yield return null;
    }

    void FixedUpdate()
    {
        this.canFire = Time.realtimeSinceStartup - this.lastFireTime > this.delayBetweenFire;
        if(this.firing && this.canFire)
        {
            this.PreFire();
            if(!this.loop)
                this.ToggleFiringOff();
        }

    }

    void Update()
    {
        if(this.test)
        {
            if(Input.GetMouseButtonDown(0))
            {
                this.Fire();
            }
        }
    }
}
