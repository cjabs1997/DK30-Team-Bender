using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("State Information")]
    [Tooltip("The current state the object is in. Visible for debugging purposes only, don't edit!!!")]
    [SerializeField] protected BaseState currentState;
    public BaseState CurrentState { get { return currentState; } }
 
    public Rigidbody2D Rigidbody2D { get; private set; }

    public Collider2D Collider2D { get; private set; }

    [SerializeField] protected Animator m_Animator;
    public Animator Animator { get { return m_Animator; } }

    [SerializeField] private StateFactory stateFactory; 
    public StateFactory PlayerStateFactory { get { return stateFactory; } }

    private void Awake()
    {
        Rigidbody2D = this.GetComponent<Rigidbody2D>();
        Collider2D = this.GetComponent<Collider2D>();
        m_Animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        currentState.EnterState(this);
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