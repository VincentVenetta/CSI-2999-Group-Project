using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{



    //Game
    public Animator animator;
    public float _speed = 5f;
    float horizontalMove = 0f;

    Vector3 characterScale;
    float characterScaleX;
    //SerializeField allows for access in the inspector 
    [SerializeField]
    public LayerMask GroundLayer;
    public Transform feet;

    
    

    // Start is called before the first frame update
    void Start()
    {   
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }



    // Update is called once per frame
    void Update()
    {
        //gets the x axis
        horizontalMove = Input.GetAxis("Horizontal");


        //jumping
        if (Input.GetKeyDown("space") && IsGrounded())
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7), ForceMode2D.Impulse);

            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        //flip the character
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }

        transform.localScale = characterScale;


    }

    //FixedUPdate is better for latency (Use for player movement)
    void FixedUpdate()
    {
        //moves the character 
        transform.position = transform.position + new Vector3(horizontalMove * _speed * Time.deltaTime, 0, 0);
        //Alternatively could do transform.position += new Vector3()

        //animates running when parameter speed is positive
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }


    //Checks for ground using the Collider2D method 
    public bool IsGrounded()
    {
        Collider2D groundcheck = Physics2D.OverlapCircle(feet.position, 0.9f, GroundLayer);

        if (groundcheck.gameObject != null)
        {
            return true;

        }
        else
        {
            return false;
        }
        
        
    }







}

