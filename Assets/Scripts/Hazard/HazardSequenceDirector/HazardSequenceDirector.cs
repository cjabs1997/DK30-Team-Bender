using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HazardSequenceDirector : MonoBehaviour
{
    private HazardController[] hazardControllers;
    public HazardController[] HazardControllers => hazardControllers;

    public UnityEvent TimerEndEventListener = new UnityEvent();

    [SerializeField]
    private HazardSequence[] hazardSequences;
    public HazardSequence[] HazardSequences => hazardSequences;

    // [SerializeField]
    // private float secondsPerCycle;
    // public float SecondsPerCycle => secondsPerCycle;

    public IEnumerator PlaySequence(HazardSequence sequence)
    {
        // sequence.Controller.PlayHazard(sequence.Hazard, 
        //                 sequence.Controller.transform.position,
        //                 sequence.Target
        //                 );
        yield return null;
    }

    public IEnumerator PlaySequences()
    {
        foreach (var sequence in hazardSequences)
        {
            yield return new WaitForSeconds(sequence.WaitBefore);
            yield return PlaySequence(sequence);
            yield return new WaitForSeconds(sequence.WaitAfter);
        }
    }

    public void onCycleEnd()
    {
        foreach(var hazard in this.hazardControllers)
        {
            hazard.CycleEnd();
        }
    }

    void Start()
    {
        hazardControllers = FindObjectsOfType<HazardController>();
        if(TimerEndEventListener != null)
            TimerEndEventListener.AddListener(onCycleEnd);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(this.PlaySequences());
        }
    }
}
