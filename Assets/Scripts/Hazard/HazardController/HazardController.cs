using UnityEngine;

public class HazardController : MonoBehaviour
{   
    [SerializeField] private Hazard hazard;
    public Hazard Hazard => hazard;
    private Rigidbody2D BaseBody;
    private Rigidbody2D Test;

    public void PlayHazard(Hazard hazard, Vector2 from, Transform to)
    {
        this.hazard = hazard;
        this.hazard.StartSequence(from, to, this);
    }

    void Start()
    {
        BaseBody = GetComponent<Rigidbody2D>();
        Test = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Test.transform.position = t;
    }

}
