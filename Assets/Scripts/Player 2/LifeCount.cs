using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LifeCount : Stomper
{


    public Image[] lives;
    public int LivesRemaining;
    public Animator animator;
    private string ENEMY_TAG = "Enemy";



    //4 lives - 4 images (0, 1, 2, 3)

    public void LoseLife()
    {
        //Decrease the value of lives remaining
        //DECREASES by 1
        LivesRemaining--;
        //Hide one of the life images
        lives[LivesRemaining].enabled = false;
        
        

    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
       
        //Animator remains false by defualt so animation resets after losing a life
        animator.SetBool("LifeLost", false);

        if (collision.gameObject.CompareTag(ENEMY_TAG) )
        {
            LoseLife();
            animator.SetBool("LifeLost", true);
           
        }
        
      

        //Check if we run out of lives and die
        if (LivesRemaining == 0)
        {
            Debug.Log("You Died");
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }


    }
        
}
