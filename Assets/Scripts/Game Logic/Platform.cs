using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] private SpriteRenderer objectSprite;


    public List<Transform> points;
    public Transform plat;
    int goalPoint = 0;
    public float moveSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();

    }

   void MoveToNextPoint()
   {
        plat.position = Vector2.MoveTowards(plat.position, points[goalPoint].position, Time.deltaTime * moveSpeed);

        if(Vector2.Distance(plat.position, points[goalPoint].position) < 0.1f)
        {
            if(goalPoint == points.Count - 1)
            {
                goalPoint = 0;

                if (gameObject.tag == "enemy")
                {
                    objectSprite.flipX = true;
                }
               
                
            }
            else
            {
                goalPoint++;

                if(gameObject.tag == "enemy")
                {
                    objectSprite.flipX = false;
                }
               
            }
        }
   }

}
