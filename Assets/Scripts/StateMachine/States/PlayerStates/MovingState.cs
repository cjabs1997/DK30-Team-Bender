using UnityEngine;
using Helpers.MovementHelpers;
using Helpers.TransitionHelpers;
using States;

[CreateAssetMenu]
public class MovingState : BaseState
{
    [SerializeField] private float maxSpeed; // Should move these to a better format
    [SerializeField] private float maxForce;
    [SerializeField] private ContactFilter2D groundMask;

    public override State stateKey { get { return State.move; } }

    private Vector2 _move;

    public MovingState(StateController newController) : base(newController)
    {

    }

    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);

        controller.Rigidbody2D.drag = 0;


    }

    public override void HandleStateTransitions()
    {
        throw new System.NotImplementedException();
    }

    public override void StateOnCollisionEnter2D(Collision2D collision)
    {

    }

    public override void StateUpdate()
    {
        //controller.Animator.SetFloat("MoveSpeed", Mathf.Abs(controller.Behavior.moveVector.x));
        _move = new Vector2(5 * Input.GetAxisRaw("Horizontal"), 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TransitionHelpers.Jump(controller.Animator);
        }

        if (MovementHelpers.CheckGround(controller, groundMask))
        {
            //controller.Animator.SetBool("Grounded", false);
            return;
        }
    }

    public override void StateFixedUpdate()
    {
       controller.Rigidbody2D.MovePosition(_move * Time.deltaTime + controller.Rigidbody2D.position);
    }

    public override void ExitState()
    {

    }
}
