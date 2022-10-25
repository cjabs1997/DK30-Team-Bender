using UnityEngine;

// These are some functions we pulled from an older State Machine iteratation, likely needs adjustments
namespace Helpers
{
    namespace MovementHelpers
    {
        static class MovementHelpers
        {
            [SerializeField] private static RaycastHit2D[] hits2D = new RaycastHit2D[1]; // Adjust this as needed, likely only need 1 tho

            public static bool CheckGround(StateController controller, ContactFilter2D groundFilter, float castLength = 0.05f)
            {
                int hitGround = controller.Collider2D.Cast(Vector2.down, groundFilter, hits2D, castLength);
                return hitGround == 0;
            }
        }
    }

    namespace TransitionHelpers
    {
        static class TransitionHelpers
        {
            public static void Jump(Animator animator)
            {
                animator.SetBool("Grounded", false);
                animator.SetBool("Jump", true);
            }

            public static void Land(Animator animator)
            {
                animator.SetBool("Grounded", true);
                animator.SetBool("Jump", false);
            }
        }
    }

}
