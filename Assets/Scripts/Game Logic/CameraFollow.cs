
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private Vector3 temPos;

    [SerializeField]
    private float minX, maxX, minY, maxY;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    
    void LateUpdate()
    {
        //Setting our vector3 = to transform and then same for x and y cords
        temPos = transform.position;
        temPos.x = player.position.x;
        temPos.y = player.position.y;

        
        //Checks to see where camera should stop following player on the x axis
        if(temPos.x < minX)
        {
            temPos.x = minX;
        }
        if (temPos.x > maxX)
        {
            temPos.x = maxX;
        }

        if (temPos.y < minY)
        {
            temPos.y = minY;
        }
        if (temPos.y > maxY)
        {
            temPos.y = maxY;
        }

        //Setting the position of the transform = to that of our vector3
        transform.position = temPos;
    }
}
