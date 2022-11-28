using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{ 
    public Transform cam;
    public float relativeMove = .3f;
    public Vector3 offset;
    public bool lockY = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      

        if (lockY)
        {
            transform.position = new Vector3(cam.position.x * relativeMove, transform.position.y) + offset;
        }
        else
        {
            transform.position = new Vector3(cam.position.x * relativeMove, cam.position.y * relativeMove) + offset;
        }
    }
}
