using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardProjectile : MonoBehaviour
{
    [SerializeField] bool rotate;
    [SerializeField] float degreesPerSecond;

    private Queue<HazardCommand> commands;
    private HazardCommand currentCommand;
    private bool waiting;

    private Vector2 CalculateVectorForce(Vector2 from, Vector2 to, float speed, float mass)
    {
        Vector2 direction = (to - from).normalized;
        float F = mass * (speed / Time.deltaTime);
        return F * direction;
    }

    private IEnumerator Wait(float seconds)
    {
        this.waiting = true;
        yield return new WaitForSeconds(seconds);
        this.waiting = false;
        this.currentCommand = null;
    }

    public void ExecuteCommands(Queue<HazardCommand> commands)
    {
        this.commands = commands;
    }
    
    private void HandleFire(FireCommand command)
    {
        var m_RigidBody = this.GetComponent<Rigidbody2D>();
        var vectorForce = this.CalculateVectorForce(command.From, (Vector2)command.To.position, command.Speed,m_RigidBody.mass);
        m_RigidBody.AddForce(vectorForce);
        this.currentCommand = null;
    }

    private void HandleMove(MoveCommand command)
    {
        var m_RigidBody = this.GetComponent<Rigidbody2D>();

        var delta = Vector2.MoveTowards(m_RigidBody.position, command.To, command.Speed * Time.deltaTime);
        m_RigidBody.MovePosition(delta);
        if(Vector2.Distance(m_RigidBody.position, command.To) <= 0.01f)
        {
            this.currentCommand = null;
        }
    }

    private void HandleWait(WaitCommand command)
    {
        if(!this.waiting)
        {
            StartCoroutine(Wait(command.Seconds));
        }
    }

    private void HandleExecuteCommand()
    {
        if(this.currentCommand == null)
        {
            if(this.commands.Count > 0)
                this.currentCommand = this.commands.Dequeue();
        }

        switch(this.currentCommand)
        {
            case FireCommand f:
            HandleFire(f);
            break;

            case MoveCommand m:
            HandleMove(m);
            break;

            case WaitCommand w:
            HandleWait(w);
            break;

            case null:
            break;
        }
    }

    void Start()
    {
        this.waiting = false;
    }
    void FixedUpdate()
    {
        HandleExecuteCommand();
    }

    void Update()
    {
        if(rotate)
        {
            this.transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
        }
        // HandleExecuteCommand();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
