using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded {get; private set;}

    // Called once per frame
    void Update()
    {
        isGrounded = CheckIfGrounded();
    }

    ///<Summary>Check a circular region under the player to see if they're on the ground.</Summary>
    private bool CheckIfGrounded()
    {
        Vector2 point = new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y - 0.55f);
        float radius = 0.45f;

        if (Physics2D.OverlapCircle(point, radius, groundLayer))
        {
            return true;
        }

        return false;
    }
}
