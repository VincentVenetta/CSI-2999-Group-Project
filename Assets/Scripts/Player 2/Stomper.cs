using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stomper : MonoBehaviour
{
    public bool Stomped = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Enemy")
        {
            
            Destroy(collision.gameObject);

            Stomped = true;

        }

       
    }

    
   


}
