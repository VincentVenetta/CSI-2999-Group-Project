using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    #region Reference Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask portalLayer;
    #endregion

    #region Collision Variables
    public bool isGrounded { get; private set; }
    public bool wallLeft { get; private set; }
    public bool wallRight { get; private set; }
    public bool touchingPortal { get; private set; }
    #endregion

    void Update()
    {
        isGrounded = CheckIfGrounded();
        wallLeft = CheckForLeftWall();
        wallRight = CheckForRightWall();
        touchingPortal = CheckIfTouchingPortal();

        


    }

    ///<Summary>Check a circular region under the player to see if they're on the ground.</Summary>
    private bool CheckIfGrounded()
    {
        Vector2 point = new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y - 0.55f);
        Vector2 size = new Vector2(0.95f, 1f);

        if (Physics2D.OverlapBox(point, size, 0f, groundLayer))
        {
            return true;

         

        }

        return false;
    }

    ///<Summary>Check to see if there is a wall on the player's left side.</Summary>
    private bool CheckForLeftWall()
    {
        Vector2 origin = new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y);

        if (Physics2D.Raycast(origin, Vector2.left, 0.55f, groundLayer))
        {
            return true;
        }

        return false;
    }

    ///<Summary>Check to see if there is a wall on the player's right side.</Summary>
    private bool CheckForRightWall()
    {
        Vector2 origin = new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y);

        if (Physics2D.Raycast(origin, Vector2.right, 0.55f, groundLayer))
        {
            return true;
        }

        return false;
    }

    ///<Summary>Check a circular region under the player to see if they're on the ground.</Summary>
    private bool CheckIfTouchingPortal()
    {
        Vector2 point = new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y);
        Vector2 size = new Vector2(1f, 2f);

        if (Physics2D.OverlapBox(point, size, 0f, portalLayer))
        {
            return true;
        }

        return false;
    }

  
}
