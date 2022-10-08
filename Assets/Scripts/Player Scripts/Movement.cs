using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Reference Variables
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private AudioManager audioManager;
    #endregion
    
    #region Movement Variables
    private float accelerationRate = 14f;
    private float decelerationRate = 1.05f;
    private float maxMovementSpeed = 10f;
    private float timeSinceLastStep = 1f;
    private float timeOnGround = 0f;
    public bool isActivelyMoving { get; private set; }
    #endregion

    #region Jumping Variables
    private float jumpForce = 17f;
    private float wallJumpForce = 27f;
    private float timeInAir = 0f;
    private bool hasDoubleJumped = false;
    #endregion

    private void Update()
    {
        Jump();
        WallJump();
        TrackAirTime();
        PlayerLanded();
        TrackGroundTime();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Decelerate();
    }

    ///<Summary>Applies an upwards impulse force when on the ground.</Summary>
    private void Jump()
    {
        //Initial jump
        if (inputManager.upPressed && collisionDetection.isGrounded)
        {
            playerRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

            audioManager.Play("Player Jump");
        }
        
        //Double jump
        if (inputManager.upPressed && !collisionDetection.isGrounded && !hasDoubleJumped
            && !collisionDetection.wallLeft && !collisionDetection.wallRight)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            playerRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

            hasDoubleJumped = true;

            audioManager.Play("Player Jump");
        }
    }

    ///<Summary>Update and store how long the player has been in the air for.</Summary>
    private void TrackAirTime()
    {
        if (!collisionDetection.isGrounded)
        {
            timeInAir += Time.deltaTime;
        }
    }

    ///<Summary>Handle variables and sound effects for when the player lands after a fall or jump.</Summary>
    private void PlayerLanded()
    {
        if (collisionDetection.isGrounded && (timeInAir >= 0.25f))
        {
            audioManager.Play("Player Landed");
            hasDoubleJumped = false;
            timeInAir = 0f;
        }
    }

    ///<Summary>Apply a force upwards and away from the wall the player is currently against while in the air.</Summary>
    private void WallJump()
    {
        if (collisionDetection.wallLeft && inputManager.upPressed && (timeInAir >= 0.25f))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            playerRigidBody.AddForce(wallJumpForce * (Vector2.up + Vector2.right).normalized, ForceMode2D.Impulse);

            audioManager.Play("Player Jump");
        }

        if (collisionDetection.wallRight && inputManager.upPressed && (timeInAir >= 0.25f))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            playerRigidBody.AddForce(wallJumpForce * (Vector2.up + Vector2.left).normalized, ForceMode2D.Impulse);

            audioManager.Play("Player Jump");
        }
    }

    ///<Summary>Applies a constant force of acceleration when the player moves left or right.</Summary>
    private void Accelerate()
    {
        if (inputManager.leftHeld && !inputManager.rightHeld)
        {
            if (playerRigidBody.velocity.x >= -maxMovementSpeed)
            {
                playerRigidBody.AddForce(accelerationRate * Vector2.left, ForceMode2D.Force);
            }

            MovingSfx();

            isActivelyMoving = true;
        }
        else if (inputManager.rightHeld && !inputManager.leftHeld)
        {
            if (playerRigidBody.velocity.x <= maxMovementSpeed)
            {
                playerRigidBody.AddForce(accelerationRate * Vector2.right, ForceMode2D.Force);
            }

            MovingSfx();

            isActivelyMoving = true;
        }
        else
        {
            isActivelyMoving = false;
        }
    }

    ///<Summary>Decelerates the player when not actively moving.</Summary>
    private void Decelerate()
    {
        if (!isActivelyMoving && (Mathf.Abs(playerRigidBody.velocity.x) > 0f))
        {
            //Snap horizontal velocity to zero when close enough
            if (Mathf.Abs(playerRigidBody.velocity.x) <= 0.05f)
            {
                playerRigidBody.velocity = new Vector2(0f, playerRigidBody.velocity.y);
            }

            MovingSfx();

            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x / decelerationRate, playerRigidBody.velocity.y);
        }
    }

    ///<Summary>Play a sound when the player is moving after a set amount of time.</Summary>
    private void MovingSfx()
    {
        //TODO make this equation less terrible and scale better
        float stepDelay = 0.90f - Mathf.Clamp((Mathf.Abs(playerRigidBody.velocity.x) / 15f), 0f, 0.75f);
        
        timeSinceLastStep += Time.deltaTime;

        if (collisionDetection.isGrounded && (timeSinceLastStep >= stepDelay) && (timeOnGround >= 0.1f))
        {
            timeSinceLastStep = 0f;
            audioManager.Play("Player Walk");
        }
    }

    ///<Summary>Update and store how long the player has been on the ground for.</Summary>
    private void TrackGroundTime()
    {
        if (collisionDetection.isGrounded)
        {
            timeOnGround += Time.deltaTime;
        }
        else
        {
            timeOnGround = 0f;
            timeSinceLastStep = 1f;
        }
    }
}
