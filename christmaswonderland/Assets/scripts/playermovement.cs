using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 5f;
    public float jumpspeed = 5f;
    [SerializeField]
    private Vector3 move;
    private float yspeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                yspeed = jumpspeed;
            }
        } else
        {
            yspeed += Physics.gravity.y * Time.deltaTime;
        }
        
        move = new Vector3(Input.GetAxis("Horizontal"), yspeed, Input.GetAxis("Vertical"));
           
    }

    
    void FixedUpdate()
    {
        characterController.Move(move*Time.deltaTime*speed);
    }
}
