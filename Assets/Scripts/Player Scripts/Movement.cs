using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private Rigidbody2D playerRigidBody;
    private float acceleration = 14f;
    private float deceleration = 1.05f;
    private float maxMovementSpeed = 10f;
    private float jumpForce = 17f;
    private bool isMoving;

    // Called once per frame
    private void Update()
    {
        Jump();
    }

    // Called every physics update
    private void FixedUpdate()
    {
        Accelerate();
        Decelerate();
    }

    ///<Summary>Applies a constant force of acceleration when the player moves left or right.</Summary>
    private void Accelerate()
    {
        if (inputManager.leftHeld && !inputManager.rightHeld)
        {
            if (playerRigidBody.velocity.x >= -maxMovementSpeed)
            {
                playerRigidBody.AddForce(acceleration * Vector2.left, ForceMode2D.Force);
            }
            isMoving = true;
        }
        else if (inputManager.rightHeld && !inputManager.leftHeld)
        {
            if (playerRigidBody.velocity.x <= maxMovementSpeed)
            {
                playerRigidBody.AddForce(acceleration * Vector2.right, ForceMode2D.Force);
            }
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    ///<Summary>Decelerates the player when stationary.</Summary>
    private void Decelerate()
    {
        if (!isMoving && (Mathf.Abs(playerRigidBody.velocity.x) > 0f))
        {
            //Snap velocity value to zero when close enough
            if (Mathf.Abs(playerRigidBody.velocity.x) <= 0.05f)
            {
                playerRigidBody.velocity = new Vector2(0f, playerRigidBody.velocity.y);
            }

            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x / deceleration, playerRigidBody.velocity.y);
        }
    }

    ///<Summary>Applies an upwards impulse force when on the ground.</Summary>
    private void Jump()
    {
        if (inputManager.upPressed && collisionDetection.isGrounded)
        {
            playerRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }
}
