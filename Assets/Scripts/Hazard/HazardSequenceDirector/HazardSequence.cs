using UnityEngine;

[System.Serializable]
public class HazardSequence
{
    [SerializeField]
    private HazardController controller;
    public HazardController Controller => controller;

    [SerializeField]
    private Transform target;
    public Transform Target => target;

    [SerializeField]
    private Hazard hazard;
    public Hazard Hazard => hazard;

    [SerializeField]
    private float waitBefore;
    public float WaitBefore => waitBefore;
    [SerializeField]
    private float waitAfter;
    public float WaitAfter => waitAfter;
}
