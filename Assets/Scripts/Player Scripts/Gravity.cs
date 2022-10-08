using UnityEngine;

public class Gravity : MonoBehaviour
{
    #region Reference Variables
    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] CollisionDetection collisionDetection;
    #endregion
    
    #region Gravity Variables
    private const float GRAVITY_CONSTANT = 9.8f;
    private const float TERMINAL_VELOCITY = -50f;
    [Range(0f, 10f)] public float gravityScale;
    #endregion

    private void FixedUpdate()
    {
        if (!collisionDetection.isGrounded)
        {
            ApplyGravity();
        }
    }

    ///<Summary>Apply a downward vertical force to the player if they aren't on the ground.</Summary>
    private void ApplyGravity()
    {
        if (playerRigidBody.velocity.y >= TERMINAL_VELOCITY)
        {
            playerRigidBody.AddForce(GRAVITY_CONSTANT * gravityScale * Vector2.down, ForceMode2D.Force);
        }
    }
}
