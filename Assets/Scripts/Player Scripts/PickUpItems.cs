using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpItems : MonoBehaviour
{
    public int collectibles = 0;
    public int collectiblesNeeded;

    [SerializeField] public Text scoreCounter;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        scoreCounter.text = "Gems: " + collectibles + "/" + collectiblesNeeded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible")){
            Destroy(collision.gameObject);
            collectibles += 1;
            scoreCounter.text = "Gems: " + collectibles + "/" + collectiblesNeeded;
            audioManager.Play("Pickup");
        }
    }

}
