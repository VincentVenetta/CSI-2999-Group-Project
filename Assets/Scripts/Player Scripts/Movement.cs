using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private AudioManager audioManager;
    private float acceleration = 14f;
    private float deceleration = 1.05f;
    private float maxMovementSpeed = 10f;
    private float jumpForce = 17f;
    private float timeSinceLastStep = 1f;
    private float timeOnGround = 0f;
    private float timeInAir = 0f;
    private bool isMoving;

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Decelerate();
    }

    ///<Summary>Applies an upwards impulse force when on the ground.</Summary>
    private void Jump()
    {
        if (inputManager.upPressed && collisionDetection.isGrounded)
        {
            playerRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            audioManager.Play("Player Jump");
        }

        if (!collisionDetection.isGrounded)
        {
            timeInAir += Time.deltaTime;
        }

        if (collisionDetection.isGrounded && (timeInAir >= 0.35f))
        {
            audioManager.Play("Player Landed");
            timeInAir = 0f;
        }
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

            MovingSfx();

            isMoving = true;
        }
        else if (inputManager.rightHeld && !inputManager.leftHeld)
        {
            if (playerRigidBody.velocity.x <= maxMovementSpeed)
            {
                playerRigidBody.AddForce(acceleration * Vector2.right, ForceMode2D.Force);
            }

            MovingSfx();

            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    ///<Summary>Play a sound when the player is moving after a set amount of time.</Summary>
    private void MovingSfx()
    {
        float stepDelay = 0.90f - Mathf.Clamp((Mathf.Abs(playerRigidBody.velocity.x) / 15f), 0f, 0.75f);
        
        timeSinceLastStep += Time.deltaTime;

        if (collisionDetection.isGrounded)
        {
            timeOnGround += Time.deltaTime;
        }
        else
        {
            timeOnGround = 0f;
            timeSinceLastStep = 1f;
        }

        if (collisionDetection.isGrounded && (timeSinceLastStep >= stepDelay) && (timeOnGround >= 0.1f))
        {
            timeSinceLastStep = 0f;
            audioManager.Play("Player Walk");
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
}
