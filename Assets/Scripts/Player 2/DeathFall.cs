using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFall : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject);
        SceneManager.LoadScene("SampleScene");
    }
}
