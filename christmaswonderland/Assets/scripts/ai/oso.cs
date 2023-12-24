using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class oso : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject player=null;
    public LayerMask whatisplayer;
    private playermovement pcont=null;
    //private CharacterScript 

    //patrolling
    bool patrolling = true;

    public float walkpointrange;
    public Vector3 walkpoint;
    bool walkpointset = false;

    float time = 0f;
    public float timedelay;
    float randomZ;
    float randomX;
    public Vector3 initpos;
    bool setPosition = true;
    //

    //for attacking
    public float difMult = 1000;
    public float attackspeed;
    float atktime = 10;
    bool attack = true;

    //para states
    public float sightrange, attackrange;
    public bool playerinsightrange = false, playerinattackrange = false;
    //

    //variables para anim 
    public Vector3 lastpos;
    public Vector3 dif;
    public SpriteRenderer sprite;
    public Animator anim;
    //

    private void Start()
    {
        //referencia para navmeshagent y player
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindWithTag("Player").transform;
        //

        //initpos = transform.position;

        //para anim
        lastpos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();

        //
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        //initpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (setPosition) { //get initial position this way because transform.position doesn't work in start
            if (transform.position != new Vector3(0, 0, 0))
            {
                initpos = transform.position;
                setPosition = false;
            }
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if(player.GetComponent<playermovement>()!=null) pcont = player.GetComponent<playermovement>(); //reference to player script
            //else if(player.GetComponent<CharacterScript>()!=null)
        }
        
        //to get change in position and to get direction

        //asignar rango de vision y ataque
        playerinsightrange = Physics.CheckSphere(transform.position, sightrange, whatisplayer);
        playerinattackrange = Physics.CheckSphere(transform.position, attackrange, whatisplayer);
        //

        if (!playerinsightrange && !playerinattackrange)//patrolling
        {
            patrolling = true;
            //buscar walkpoint patrolling
            if (!walkpointset)
            {
                randomZ = Random.Range(-walkpointrange, walkpointrange);
                randomX = Random.Range(-walkpointrange, walkpointrange);

                walkpoint = new Vector3(initpos.x + randomX, initpos.y, initpos.z + randomZ);
                agent.destination = walkpoint;
                walkpointset = true;
            }
            //
        }
        else if (playerinsightrange && !playerinattackrange)//chaseplayer
        {
            if (patrolling) patrolling = false;
            //player = GameObject.FindWithTag("Player").transform;
            //agent.destination = player.position;
            agent.destination = player.transform.position;
            walkpointset = false;
        }
        else if (playerinsightrange && playerinattackrange && attack && !pcont.dead)//attackplayer
        {
            //player = GameObject.FindWithTag("Player").transform;
            //agent.destination = player.position;

            pcont.setYSpeed(pcont.jumpspeed);
            pcont.setAttackDir(dif.x*difMult,dif.z*difMult);
            pcont.setAttacked(true);
            pcont.life--;
            //Debug.Log("enemyHasAttacked");
            atktime = 0f;
            
        }


        if (atktime <= attackspeed)//setting timer or delay
        {
            attack = false;
            atktime = atktime + 1f * Time.deltaTime;
        }
        else
        {
            attack = true;
        }
        /*if (patrolling)
        {

            
        }*/

        //para anim
        dif = transform.position - lastpos; 

        if (dif.z > 0)
        {
            lastpos = transform.position;
            anim.SetFloat("blend", 1);
            sprite.flipX = true;
        }
        else if (dif.z < 0)
        {
            lastpos = transform.position;
            anim.SetFloat("blend", 1);
            sprite.flipX = false;
        }
        else if (dif.z == 0)
        {
            anim.SetFloat("blend", 0);
            //


            if (patrolling)
            {
                //wait for delay to start searching for walkpoint again patrolling
                if (time <= timedelay)
                {
                    time = time + 1f * Time.deltaTime;
                }
                else
                {
                    walkpointset = false;
                    time = 0f;
                }
                //
            }
        }

    } 

    private void OnDrawGizmos()
    {
    }
}
