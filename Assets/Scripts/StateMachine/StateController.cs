using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("State Information")]
    [Tooltip("The current state the object is in. Visible for debugging purposes only, don't edit!!!")]
    [SerializeField] protected BaseState currentState;
    public BaseState CurrentState { get { return currentState; } }

    [SerializeField] protected Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }

    [SerializeField] protected Collider2D m_Collider2D;
    public Collider2D Collider2D { get { return m_Collider2D; } }

    [SerializeField] protected Animator m_Animator;
    public Animator Animator { get { return m_Animator; } }

    //[SerializeField] protected PlayerStats stats;
    //public PlayerStats Stats { get { return stats; } } // Will likely add this later, 

    [SerializeField] private StateFactory stateFactory; 
    public StateFactory PlayerStateFactory { get { return stateFactory; } }

    private void Awake()
    {
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_Collider2D = this.GetComponent<Collider2D>();
        m_Animator = this.GetComponent<Animator>();

        //currentState = playerStateFactory.States[PlayerStateFactory.PlayerStates.idle]; // There might be a more elegant solution than this
    }

    private void Update()
    {
        //behavior.GetCommand();
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.StateOnCollisionEnter2D(collision);
    }

    public void TransitionToState(BaseState state)
    {
        currentState.ExitState();
        state.EnterState(this);
        currentState = state;
    }
}