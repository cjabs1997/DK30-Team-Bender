using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSequenceDirector : MonoBehaviour
{
    private HazardController[] hazardControllers;
    public HazardController[] HazardControllers => hazardControllers;

    [SerializeField]
    private HazardSequence[] hazardSequences;
    public HazardSequence[] HazardSequences => hazardSequences;

    public IEnumerator PlaySequence(HazardSequence sequence)
    {
        sequence.Controller.PlayHazard(sequence.Hazard, 
                        sequence.Controller.transform.position,
                        sequence.Target
                        );
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

    void Start()
    {
        hazardControllers = FindObjectsOfType<HazardController>();
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
