using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{
    public int collectables = 0;

    [SerializeField] public Text scoreCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collectable")){
            Destroy(collision.gameObject);
            collectables += 1;
            scoreCounter.text = "Score: " + collectables;
        }
    }

}
