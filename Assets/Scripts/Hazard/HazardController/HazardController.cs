using UnityEngine;

public class HazardController : MonoBehaviour
{   
    [SerializeField] private Hazard hazard;
    public Hazard Hazard => hazard;
    [SerializeField]
    private Transform to;
    public Transform To => to;
    [SerializeField]
    private bool cycles;
    public bool Cycles => cycles;
    [SerializeField]
    private int cyclesToFire;
    public int CyclesToFire => cyclesToFire;
    private int cyclesPassed = 0;
    private Rigidbody2D BaseBody;
    private AudioSource audioSource;
    [SerializeField]
    HazardCommand[] testList;
    

    [SerializeField]
    private SimpleAudioEvent audioEventShoot;
    public SimpleAudioEvent AudioEventShoot => audioEventShoot;
    
    public void CycleEnd()
    {
        if(!cycles) return;
        cyclesPassed++;
        if(cyclesPassed >= cyclesToFire)
        {
            this.PlayHazard();
            cyclesPassed = 0;
        }
    }

    public void PlayHazard(Hazard hazard, Vector2 from, Transform to)
    {
        this.hazard = hazard;
        this.hazard.StartSequence(from, to, this);
    }

    public void PlayHazard()
    {
        this.hazard.StartSequence(this.transform.position, this.to, this);
    }

    public void PlayShootSound()
    {
        this.audioEventShoot.Play(this.audioSource);
    }

    void Start()
    {
        BaseBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

}
