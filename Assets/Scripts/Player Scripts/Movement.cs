using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    #region Reference Variables
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private PickUpItems pickupitems;



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
        //Disables movement during dialogue, but allows free movement until level 7
        if (dialogue.canMove || SceneManager.GetActiveScene().buildIndex < 7)
        {
            Jump();
            WallJump();
            TrackAirTime();
            PlayerLanded();
            TrackGroundTime();
            EnterNextLevel();
        }
    }

    private void FixedUpdate()
    {
        //Disables movement during dialogue, but allows free movement until level 7
        if (dialogue.canMove || SceneManager.GetActiveScene().buildIndex < 7)
        {
            Accelerate();
            Decelerate();
        }
       
    }

    ///<Summary>Applies an upwards impulse force when on the ground.</Summary>
    private void Jump()
    {
        //Lock jumping until level 3
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            return;
        }

        //Initial jump
        if (inputManager.upPressed && collisionDetection.isGrounded)
        {
            playerRigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

            //Lock sfx until level 4
            if (SceneManager.GetActiveScene().buildIndex >= 4)
            {                
                audioManager.Play("Player Jump");
            }
        }
        
        //Lock double jumping until level 5
        if (SceneManager.GetActiveScene().buildIndex < 5)
        {
            return;
        }

        //Double jump
        if (inputManager.upPressed && !collisionDetection.isGrounded && !hasDoubleJumped)
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
            //Lock sfx until level 4
            if (SceneManager.GetActiveScene().buildIndex >= 4)
            {
                audioManager.Play("Player Landed");
            }
        
            hasDoubleJumped = false;
            timeInAir = 0f;
        }
    }

    ///<Summary>Apply a force upwards and away from the wall the player is currently against while in the air.</Summary>
    private void WallJump()
    {
        //Lock wall jumping until level 6
        if (SceneManager.GetActiveScene().buildIndex < 6)
        {
            return;
        }

        if (collisionDetection.wallLeft && inputManager.upPressed && (timeInAir >= 0.25f))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            playerRigidBody.AddForce(wallJumpForce * (Vector2.up + Vector2.right).normalized, ForceMode2D.Impulse);
            hasDoubleJumped = false;

            audioManager.Play("Player Jump");
        }

        if (collisionDetection.wallRight && inputManager.upPressed && (timeInAir >= 0.25f))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            playerRigidBody.AddForce(wallJumpForce * (Vector2.up + Vector2.left).normalized, ForceMode2D.Impulse);
            hasDoubleJumped = false;

            audioManager.Play("Player Jump");
        }
    }

    ///<Summary>Applies a constant force of acceleration when the player moves left or right.</Summary>
    private void Accelerate()
    {
        if (inputManager.leftHeld && !inputManager.rightHeld)
        {
            //Lock leftward movement until level 2
            if (SceneManager.GetActiveScene().buildIndex < 2)
            {
                return;
            }

            if (playerRigidBody.velocity.x >= -maxMovementSpeed)
            {
                playerRigidBody.AddForce(accelerationRate * Vector2.left, ForceMode2D.Force);
            }

            //Lock sfx until level 4
            if (SceneManager.GetActiveScene().buildIndex >= 4)
            {
                MovingSfx();
            }

            isActivelyMoving = true;
        }
        else if (inputManager.rightHeld && !inputManager.leftHeld)
        {

            if (playerRigidBody.velocity.x <= maxMovementSpeed)
            {
                playerRigidBody.AddForce(accelerationRate * Vector2.right, ForceMode2D.Force);
            }
            
            //Lock sfx until level 4
            if (SceneManager.GetActiveScene().buildIndex >= 4)
            {
                MovingSfx();
            }

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

            //Lock sfx until level 4
            if (SceneManager.GetActiveScene().buildIndex >= 4)
            {
                MovingSfx();
            }

            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x / decelerationRate, playerRigidBody.velocity.y);
        }
    }

    ///<Summary>Play a sound when the player is moving after a set amount of time.</Summary>
    private void MovingSfx()
    {
        //TODO make this equation less terrible
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

    ///<Summary>Execute code requried to enter the next level.</Summary>
    private void EnterNextLevel()
    {
        //PROBABLY A BETTER WAY TO WRITE THIS CODE THAN COPY AND PASTE
        if(SceneManager.GetActiveScene().buildIndex <= 8)
        {
            //enters next level if all collectables are picked or there are no collectables
            if (collisionDetection.touchingPortal)
            {

                //Lock sfx until level 4
                if (SceneManager.GetActiveScene().buildIndex >= 4 && (levelLoader.portalSoundPlayed == false))
                {
                    levelLoader.portalSoundPlayed = true;
                    audioManager.Play("Portal");
                }

                levelLoader.LoadNextLevel();


            }

        }
        else
        {
            //enters next level if all collectables are picked or there are no collectables
            if (collisionDetection.touchingPortal && pickupitems.collectables == 3)
            {

                //Lock sfx until level 4
                if (SceneManager.GetActiveScene().buildIndex >= 4 && (levelLoader.portalSoundPlayed == false))
                {
                    levelLoader.portalSoundPlayed = true;
                    audioManager.Play("Portal");
                }

                levelLoader.LoadNextLevel();


            }
        }
       
     
    }
}
