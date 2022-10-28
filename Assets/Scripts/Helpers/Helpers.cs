using UnityEngine;

// These are some functions we pulled from an older State Machine iteratation, likely needs adjustments
namespace Helpers
{
    namespace MovementHelpers
    {
        static class MovementHelpers
        {
            [SerializeField] private static RaycastHit2D[] hits2D = new RaycastHit2D[1]; // Adjust this as needed, likely only need 1 tho

            public static bool CheckGround(StateController controller, ContactFilter2D groundMask, float castLength = 0.05f)
            {
                int hitGround = controller.Collider2D.Cast(Vector2.down, groundMask, hits2D, castLength);
                return hitGround == 1;
            }

            static public Vector2 LateralMove(StateController controller, float maxForce, float maxSpeed, float moveDir)
            {
                float horizontal_force = GetHorizontalForce(controller, maxSpeed, moveDir);

                Vector2 moveVector = controller.transform.right * horizontal_force;

                moveVector = Vector2.ClampMagnitude(moveVector, maxForce);

                //Debug.DrawRay(controller.transform.position, moveVector, Color.red);
                return moveVector;
            }

            private static float GetHorizontalForce(StateController controller, float maxSpeed, float moveDir)
            {
                if (moveDir == 0) return 0;

                float targetVelocity = maxSpeed * moveDir;

                // Vf = Vi + A * T
                float acceleration = (targetVelocity - controller.Rigidbody2D.velocity.x) / Time.deltaTime;

                // F = M * A
                float moveForce = controller.Rigidbody2D.mass * acceleration;

                Mathf.Min(moveForce, Mathf.Max(0, moveForce));

                return moveForce;
            }
        }
    }

    namespace TransitionHelpers
    {
        
    }

}
