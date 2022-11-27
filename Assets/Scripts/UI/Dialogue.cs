using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool canMove;

    public GameObject DialogueBox;
    public GameObject continueButton;

    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        //shows the button after each sentence
        if(textDisplay.text != null)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }
        
    }

    IEnumerator Type()
    {
        //loops through each char making the animation effect of dialogue
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

   

    public void NextSentence()
    {
        //hides the button while dialogue is being written
        continueButton.SetActive(false);

        //checks if sentence has ended, if so clears and starts new sentence
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            DialogueBox.SetActive(false);
            canMove = true;


        }
    }


}
