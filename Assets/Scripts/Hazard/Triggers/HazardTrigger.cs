using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private GameEvent onPlayerEnter;
    public GameEvent OnPlayerEnter => onPlayerEnter;
    [SerializeField]
    private GameEvent onPlayerExit;
    public GameEvent OnPlayerExit => onPlayerExit;
    [SerializeField]
    private float delayFireOnEnter;
    public float DelayFireOnEnter => delayFireOnEnter;
    [SerializeField]
    private SimpleAudioEvent soundOnTrigger;
    public SimpleAudioEvent SoundOnTrigger => soundOnTrigger;
    [SerializeField]
    private bool triggerOnce;
    public bool TriggerOnce => triggerOnce;
    #endregion
    private AudioSource audioSource;
    private Collider2D triggerCollider;
    private bool waiting;
    private bool canTrigger;
    void Start()
    {
        this.triggerCollider = GetComponent<Collider2D>();
        this.audioSource = GetComponent<AudioSource>();
        this.waiting = false;
        this.canTrigger = true;
    }

    public void EnableTrigger()
    {
        this.canTrigger = true;
    }

    public void DisableTrigger()
    {
        this.canTrigger = false;
    }

    private IEnumerator WaitForTrigger(Collider2D otherCollider)
    {
        this.waiting = true;
        yield return new WaitForSeconds(delayFireOnEnter);
        if(this.triggerCollider.IsTouching(otherCollider))
        {
            if(this.soundOnTrigger)
            {
                this.soundOnTrigger.Play(this.audioSource);
                yield return new WaitWhile(() => this.audioSource.isPlaying);
            }
            if(this.onPlayerEnter)
                this.onPlayerEnter.Raise();
        }
        this.waiting = false;
        if(this.triggerOnce)
        {
            this.DisableTrigger();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!this.canTrigger) return;
        if(collider.TryGetComponent<PlayerStateController>(out PlayerStateController stateController))
        {
            // this.onPlayerEnter.Raise();
            if(!this.waiting)
                StartCoroutine(WaitForTrigger(collider));
        }
    }

     void OnTriggerExit2D(Collider2D collider)
    {
        if(!this.canTrigger) return;
        if(collider.TryGetComponent<PlayerStateController>(out PlayerStateController stateController))
        {
            if(this.onPlayerExit)
                this.onPlayerExit.Raise();
        }
    }
}
