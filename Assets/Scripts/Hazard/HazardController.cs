using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{   
    [SerializeField] private Hazard hazard;
    public Hazard Hazard => hazard;
    private Rigidbody2D BaseBody;

    void Start()
    {
        BaseBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // hardcode for testing
        // this.projectedMousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // var t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // data.to.Set(t.x, t.y);
        if(Input.GetMouseButtonDown(0))
        {
            // Reason for struct is to keep references to to and from during firings

            hazard.StartSequence(BaseBody.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), this);
        }
    }

    void FixedUpdate()
    {
        
    }

}
