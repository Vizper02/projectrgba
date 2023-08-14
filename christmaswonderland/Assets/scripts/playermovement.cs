using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    private CharacterController characterController;
    
    public Animator animator;
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public RuntimeAnimatorController anim3;
    public RuntimeAnimatorController anim4;
    public RuntimeAnimatorController anim5;
    public RuntimeAnimatorController anim6;
    
    private int charselec = 1;
    public float speed;
    public float jumpspeed = 5f;
    [SerializeField]
    private Vector3 move;
    private float horizontal = 0f;
    private float vertical = 0f;
    private bool ch = false; 
    private bool cv = false;

    //for gravity and jumping
    private float yspeed = 0f;
    private bool hasGravity = false;
    private bool hadGravity = false;
    [SerializeField]private float time = 0f;
    public float timeDelayYReset;
    public float yspeedReset = -.1f;
    //

    //for water
    bool inWater = false;
    public float waterGrav = -1.5f;
    public float uWatVelMaxP = -2f;
    public float watDensity;
    //

    //for pickUps
    private short pickUpCount = 0;

    private Vector3 scalet;

    //when attacked
    [SerializeField] private Vector3 attackdir = new Vector3(0,0,0);
    bool attacked = false;
    bool resetAtk = false;
    public byte life = 3;
    public bool dead = false;
    public Canvas canvas;
    PanelScript gOver;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        scalet = transform.localScale;

        canvas = FindObjectOfType<Canvas>();
        gOver = canvas.GetComponentInChildren<PanelScript>(true);

        //para seleccionar personaje
        speed = PlayerPrefs.GetFloat("speed");
        charselec = PlayerPrefs.GetInt("CharacterSelected");
      
        if (charselec == 1)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;
        } else if(charselec == 2)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        } else if(charselec == 3)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim3 as RuntimeAnimatorController;
        } else if (charselec == 4)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim4 as RuntimeAnimatorController;
        } else if (charselec == 5)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim5 as RuntimeAnimatorController;
        } else if (charselec == 6)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim6 as RuntimeAnimatorController;
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        if (!dead)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal=0;
            vertical=0;
        }

        

        //applymovement
        if(horizontal < 0f)//to flip sprite
        {
            transform.localScale = new Vector3(-scalet.x, scalet.y, scalet.z);
        }else if(horizontal > 0f)
        {
            transform.localScale = scalet;
        }

        if(!attacked)
        {
            move = new Vector3(vertical, yspeed, -horizontal);//key statement referenced on FixedUpdate()
        } 
        else
        {
            move = new Vector3(attackdir.x, yspeed, attackdir.z);
        }

        //jumping and gravity
        if (characterController.isGrounded&&!inWater)
        {
            hasGravity = false;
            if (hadGravity)
            {
                if (time < timeDelayYReset)
                {
                    time = time + 1f * Time.deltaTime;
                }
                else
                {
                    yspeed = yspeedReset;
                    time = 0f;
                    hadGravity = false;
                }
            }

            if (Input.GetButtonDown("Jump")&&!dead)
            {
                yspeed = jumpspeed;
            }
            if (resetAtk)
            {
                attacked = false;
                resetAtk = false;
            }
        } else if (!inWater)
        {
            hasGravity = true;
            hadGravity = true;
            if (attacked)
            {
                resetAtk = true;
            }
        }
        //when entering Water
        else
        {
            if (Input.GetButtonDown("Jump") && !dead)
            {
                yspeed = jumpspeed;
            }
        }
      
        
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

        if(life <= 0)
        {
            dead = true;
            life = 1;
            gOver.enable();
        }
        
    }

    void FixedUpdate()
    {
        if (hasGravity&&!inWater) 
        {
            yspeed += Physics.gravity.y * Time.deltaTime;
        }
        else if (inWater)
        {
            if (yspeed > uWatVelMaxP) yspeed += waterGrav * Time.deltaTime;
            else yspeed += (uWatVelMaxP-yspeed)*Time.deltaTime*watDensity;
        }

        characterController.Move(move*Time.deltaTime*speed);
    }









    




    //for water
    public void setWaterMode(bool water)
    {
        inWater = water;
    }

    public float getSpeed()
    {
        return speed;
    }
    
    public void setSpeed(float sp)
    {
        speed = sp;
    }
    //


    //for bear attacks
    public void setYSpeed(float yspeed)
    {
        this.yspeed = yspeed;
    }

    public void setAttackDir(float x, float z)
    {
        this.attackdir.x = x;
        this.attackdir.z = z;
    }

    public void setAttacked(bool attacknew)
    {
        this.attacked = attacknew;
    }
    //

    //for Pick Ups
    public void increasePickUps(float speedIncrease)
    {
        pickUpCount++;
        StartCoroutine(speedTimer(speedIncrease));
    }

    IEnumerator speedTimer(float speedIncrease)
    {
        speed *= speedIncrease;
        Debug.Log(speed);
        yield return new WaitForSeconds((float)2.5);
        speed /= speedIncrease;
        Debug.Log(speed);
    }
    //
}
