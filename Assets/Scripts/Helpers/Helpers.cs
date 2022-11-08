using UnityEngine;

// These are some functions we pulled from an older State Machine iteratation, likely needs adjustments
namespace Helpers
{
    namespace MovementHelpers
    {
        static class MovementHelpers
        {
            [SerializeField] private static RaycastHit2D[] hits2D = new RaycastHit2D[1]; // Adjust this as needed, likely only need 1 tho

            public static bool CheckGround(Collider2D collider, ContactFilter2D groundMask, float castLength = 0.05f)
            {
                int hitGround = collider.Cast(Vector2.down, groundMask, hits2D, castLength);
                return hitGround == 1;
            }

            static public Vector2 LateralMove(Rigidbody2D rigidbody, float maxForce, float maxSpeed, float moveDir)
            {
                float horizontal_force = GetHorizontalForce(rigidbody, maxSpeed, moveDir);

                Vector2 moveVector = rigidbody.transform.right * horizontal_force;

                moveVector = Vector2.ClampMagnitude(moveVector, maxForce);

                //Debug.DrawRay(controller.transform.position, moveVector, Color.red);
                return moveVector;
            }

            private static float GetHorizontalForce(Rigidbody2D rigidbody, float maxSpeed, float moveDir)
            {
                if (moveDir == 0) return 0;

                float targetVelocity = maxSpeed * moveDir;

                // Vf = Vi + A * T
                float acceleration = (targetVelocity - rigidbody.velocity.x) / Time.deltaTime;

                // F = M * A
                float moveForce = rigidbody.mass * acceleration;

                //Mathf.Min(moveForce, Mathf.Max(0, moveForce));

                return moveForce;
            }
        }
    }

    namespace TransitionHelpers
    {
        
    }

}
