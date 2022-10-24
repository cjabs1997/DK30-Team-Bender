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
        if(Input.GetMouseButtonDown(0)){
            var relativeMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hazard.StartSequence(this, BaseBody.position, relativeMousePosition);
        }
    }

    void FixedUpdate()
    {
        
    }

}
