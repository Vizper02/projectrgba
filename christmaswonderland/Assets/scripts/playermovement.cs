using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private CharacterController characterController;

    public Animator animator;
    public float speed = 5f;
    public float jumpspeed = 5f;
    [SerializeField]
    private Vector3 move;
    private float horizontal = 0f;
    private float vertical = 0f;
    private bool ch = false; 
    private bool cv = false;
    private float yspeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //jumping
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                yspeed = jumpspeed;
            }
        } else
        {
            yspeed += Physics.gravity.y * Time.deltaTime;
        }

        //applymovement
        move = new Vector3(horizontal, yspeed, vertical);
        
        //animation
        if(horizontal != 0f)
        {
            ch = true;
        } else
        {
            ch = false;
        }
        if(vertical != 0f)
        {
            cv = true;
        } else
        {
            cv = false;
        }
        if(yspeed > 0f)
        {
            animator.SetBool("isjumping", true);
        } else
        {
            animator.SetBool("isjumping", false);
        }
        if (ch || cv)
        {
            animator.SetFloat("blend", 1);
        } else
        {
            animator.SetFloat("blend", 0);
        }


    }

    
    void FixedUpdate()
    {
        characterController.Move(move*Time.deltaTime*speed);
    }
}
