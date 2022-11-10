using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private GameEvent onTimerEnd;
    public GameEvent OnTimerEnd => onTimerEnd;

    [SerializeField]
    private float countDownLength;
    public float CountdownLength => countDownLength;
    [SerializeField]
    private bool startOnAwake;
    public bool StartOnAwake => startOnAwake;
    [SerializeField]
    private bool loop;
    public bool Loop => loop;
    private float startTime;
    private bool isCountingDown;
    private float secondsElapsed;
    public float SecondsElapsed => secondsElapsed;

    public void StartCountdown()
    {  
        startTime = Time.realtimeSinceStartup;
        this.secondsElapsed = 0;
        this.isCountingDown = true;
    }

    public void StopCountdown()
    {
        this.secondsElapsed = 0;
        this.isCountingDown = false;
        startTime = Time.realtimeSinceStartup;
        if(OnTimerEnd != null)
            OnTimerEnd.Raise();
    }

    public void PauseCountdown()
    {
        this.isCountingDown = false;
    }

    public void ResumeCountdown()
    {
        this.isCountingDown = true;
    }

    public void Reset()
    {
        this.StopCountdown();
        this.StartCountdown();
    }

    void Start()
    {
        this.isCountingDown = false;
        this.secondsElapsed = 0;
        this.startTime = Time.realtimeSinceStartup;
        if(this.StartOnAwake)
        {
            this.StartCountdown();
        }
    }

    void FixedUpdate()
    {
        if(!this.isCountingDown) return;
        this.secondsElapsed = Time.realtimeSinceStartup - startTime;
        if(this.secondsElapsed > this.CountdownLength)
        {
            if(this.Loop){
                this.Reset();
                return;
            }
            this.StopCountdown();
        }
    }

    void Update()
    {
        // Debug.Log(this.secondsElapsed);
    }
}
