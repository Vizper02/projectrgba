using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    private Vector3 scalet;
    
    
    public float speed;
    public float jumpspeed = 5f;
    public FloatingJoystick joystick; 
    [SerializeField] private Vector3 move;
    private float horizontal = 0f;
    private float vertical = 0f;
    private bool ch = false; 
    private bool cv = false;
    private int charselec = 1;

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
    public short pickUpCount = 0;
    //

    

    //when attacked
    [SerializeField] private Vector3 attackdir = new Vector3(0,0,0);
    bool attacked = false;
    bool resetAtk = false;
    public byte life = 1;
    public bool dead = false;
    public Canvas canvas;

    //UI
    PanelScript finalPanel;
    GameObject fJoy;
    GameObject pauseButton;
    bool isPause = false;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        scalet = transform.localScale;

        canvas = FindObjectOfType<Canvas>();
        finalPanel = canvas.GetComponentInChildren<PanelScript>(true);

        joystick = canvas.GetComponentInChildren<FloatingJoystick>(true);
        fJoy = canvas.transform.GetChild(1).gameObject;
        pauseButton = canvas.transform.GetChild(2).gameObject;

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

    void Awake() {
        isPause = false;
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!dead && !isPause)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            if (joystick != null)
            {
                if (joystick.Horizontal > .9) horizontal = 1;
                else if (joystick.Horizontal < -.9) horizontal = -1;
                else horizontal = joystick.Horizontal;
                if (joystick.Vertical > .9) vertical = 1;
                else if (joystick.Vertical < -.9) vertical = -1;
                else vertical = joystick.Vertical;
            }
        }
        else if(dead)//case I am dead
        {
            horizontal = 0;
            vertical = 0;
            if (fJoy != null) fJoy.SetActive(false); //disabling joystick when dead
        }

        

        //to flip sprite
        if(horizontal < 0f)
        {
            transform.localScale = new Vector3(-scalet.x, scalet.y, scalet.z);
        }else if(horizontal > 0f)
        {
            transform.localScale = scalet;
        }

        //to apply movement
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

            //jumping
            if (Input.GetButtonDown("Jump")&&!dead&&!isPause)
            {
                yspeed = jumpspeed;
            }
            for (int i = 0; i < Input.touchCount&&!dead&&!isPause; i++) {
                Touch touch = Input.GetTouch(i);
                float xPos = touch.position.x, yPos = touch.position.y;
                bool pauseZone = false;
                if (xPos > (float)(Screen.width * .92) && yPos > (float)(Screen.height * .8)) pauseZone = true;
                if (xPos > (float)(Screen.width * .6)&&!pauseZone) yspeed = jumpspeed;
            }
            //

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
            if (Input.GetButtonDown("Jump") && !dead&&!isPause)
            {
                yspeed = jumpspeed;
            }

            for (int i = 0; i < Input.touchCount && !dead; i++)
            {
                Touch touch = Input.GetTouch(i);
                float xPos = touch.position.x, yPos = touch.position.y;
                bool pauseZone = false;
                if (xPos > (float)(Screen.width * .92) && yPos > (float)(Screen.height * .8)) pauseZone = true;
                if (xPos > (float)(Screen.width * .6) && !pauseZone) yspeed = jumpspeed;
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
            finalPanel.enable();
            finalPanel.setTextColor(false);
        }
        
        if(pickUpCount >= 100)
        {
            dead = true;
            pickUpCount = 0;
            pauseButton.SetActive(false);
            finalPanel.enable();
            finalPanel.setTextColor(true);
        }
    }

    void FixedUpdate()
    {

        if (hasGravity&&!inWater) 
        {
            yspeed += Physics.gravity.y * Time.deltaTime;//applying gravity
        }
        else if (inWater)
        {
            if (yspeed > uWatVelMaxP) yspeed += waterGrav * Time.deltaTime;
            else yspeed += (uWatVelMaxP-yspeed)*Time.deltaTime*watDensity;
        }

        characterController.Move(move*Time.deltaTime*speed);//apply movement
    }













    //for UI, (pause function is in PauseScript)
    public void setIsPause(bool val) {
        this.isPause = val;
    }
    public bool getIsPause() {
        return isPause;
    }
    //


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

    public void speedBoost(float speedIncrease)
    {
        StartCoroutine(speedTimer(speedIncrease));
    }

    IEnumerator speedTimer(float speedIncrease)
    {
        speed += speedIncrease;
        yield return new WaitForSeconds((float)3);
        speed -= speedIncrease;
    }
    //
}
