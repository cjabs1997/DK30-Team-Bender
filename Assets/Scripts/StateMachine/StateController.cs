using UnityEngine;

public abstract class StateController<T1, T2> : MonoBehaviour
{
    [Header("State Information")]
    [Tooltip("The current state the object is in. Visible for debugging purposes only, don't edit!!!")]
    [SerializeField] protected T1 currentState;
    public T1 CurrentState { get { return currentState; } }
 
    public Rigidbody2D Rigidbody2D { get; private set; }

    public Collider2D Collider2D { get; private set; }

    [SerializeField] protected Animator m_Animator;
    public Animator Animator { get { return m_Animator; } }

    [SerializeField] private T2 stateFactory; 
    public T2 PlayerStateFactory { get { return stateFactory; } }

    private void Awake()
    {
        Rigidbody2D = this.GetComponent<Rigidbody2D>();
        Collider2D = this.GetComponent<Collider2D>();
        m_Animator = this.GetComponent<Animator>();
    }

    public abstract void TransitionToState(T1 state);
}