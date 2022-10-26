using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{   
    [SerializeField] private Hazard hazard;
    public Hazard Hazard => hazard;
    private Rigidbody2D BaseBody;
    private Rigidbody2D Test;

    void Start()
    {
        BaseBody = GetComponent<Rigidbody2D>();
        Test = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // hardcode for testing
        // this.projectedMousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Test.transform.position = t;
        // data.to.Set(t.x, t.y);
        if(Input.GetMouseButtonDown(0))
        {
            hazard.StartSequence(BaseBody.position, Test.transform, this);
        }
    }

    void FixedUpdate()
    {
        
    }

}
