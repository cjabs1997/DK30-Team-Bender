using UnityEngine;

public abstract class StateController<T1, T2> : MonoBehaviour
{
    [Header("State Information")]
    [Tooltip("The current state the object is in. Visible for debugging purposes only, don't edit!!!")]
    [SerializeField] protected T1 currentState;
    public T1 CurrentState { get { return currentState; } }
 
    public Rigidbody2D Rigidbody2D { get; protected set; }

    public Collider2D Collider2D { get; protected set; }

    public AudioSource AudioSource { get; protected set; }

    [SerializeField] private T2 stateFactory; 
    public T2 StateFactory { get { return stateFactory; } }

    protected virtual void Awake()
    {
        Rigidbody2D = this.GetComponent<Rigidbody2D>();
        Collider2D = this.GetComponent<Collider2D>();
        AudioSource = this.GetComponent<AudioSource>();
    }

    public abstract void TransitionToState(T1 state);
}