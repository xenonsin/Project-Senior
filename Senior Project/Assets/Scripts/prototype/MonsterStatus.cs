using UnityEngine;
using System.Collections;

public class MonsterStatus : MonoBehaviour {

    public int health = 100;
    private Animation anim;
    //private float distance;
    private float rotateSpeed = 10f;
    private Vector3 spawnLocation;
    //private float movespeed = 0.25f;// * Time.deltaTime;
    private float aggroRadius = 2f;
    //private float meleerange = 0.5f;
    //private Vector3 playerpos = Vector3.zero;
    //private bool attackcooldown = false;
    //private bool dmganimation = false;

    private enum State
    {
        Idle,
        Run,
        Attack,
        TakeDmg,
        Dead
    }

    private State currentState = State.Idle;

    void StateMachine()
    {
        switch (currentState)
        {
            case State.Idle:
                idle_state();
                break;
            case State.Run:
                run_state();
                break;
            //case State.Attack:
            //    atk_state();
            //    break;
            //case State.TakeDmg:
            //    dmg_state();
            //    break;
            //case State.Dead:
            //    dead_state();
            //    break;
        }
    }

    void idle_state()
    {
        //animation
        anim.Play("Idle");

        //if player is within aggro range go to run state
        if (playerInRange != null)
            currentState = State.Run;
    }

    void run_state()
    {
        //animation
        anim.Play("Idle");

    //    //move towards the player until melee range
    //    if (distance > meleerange)
    //        transform.position = Vector3.Lerp(transform.position, playerpos, movespeed);
    //    else
    //        currentState = State.Attack;

        //if player is out of aggro range, go back to idle
        if (playerInRange == null)
            currentState = State.Idle;
        else
        {
            //look at the player
            Vector3 pos = playerInRange.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }

    //void atk_state()
    //{
    //    //attack once
    //    if (!attackcooldown)
    //    {
    //        anim["Attack"].speed = 0.5f;
    //        anim.Play("Attack");
    //        attackcooldown = true;
    //    }

    //    //if player is out of melee range, go to run state
    //    if (distance > meleerange)
    //        currentState = State.Run;

    //    //reset attackcooldown
    //    if(!anim.IsPlaying("Attack"))
    //        attackcooldown = false;
    //}

    //void dmg_state()
    //{
    //    if (!dmganimation)
    //    {
    //        anim.Play("TakeDmg");
    //        dmganimation = true;
    //    }
    //    if (!anim.IsPlaying("TakeDmg"))
    //    {
    //        currentState = State.Idle;
    //        dmganimation = false;
    //    }
    //}

    private bool deadanimation = false;

    void dead_state()
    {
        //play dead animation once
        if (!deadanimation)
        {
            anim["Die"].speed = 0.5f;
            anim.Play("Die");
            deadanimation = true;
        }

        //destroy gameobject after dead animation is finished
        if (!anim.IsPlaying("Die"))
            Destroy(gameObject);
    }

    private GameObject aggroRange;
    public GameObject playerInRange;

    void Awake()
    {
        //initialization and creating aggroDetector
        anim = GetComponent<Animation>();
        aggroRange = new GameObject("aggroRange");
        aggroRange.transform.position = gameObject.transform.position;
        aggroRange.transform.SetParent(gameObject.transform);
        aggroRange.AddComponent<SphereCollider>();
        aggroRange.GetComponent<SphereCollider>().isTrigger = true;
        aggroRange.GetComponent<SphereCollider>().radius = aggroRadius;

        //recording spawnLocation
        spawnLocation = gameObject.transform.position;
    }

    //aggroDetector Behaviors
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            if(playerInRange==null)
                playerInRange = col.gameObject;
    }
    //void OnTriggerStay(Collider col)
    //{
    //    if (playerInRange==null)
    //        if (col.tag == "Player")
    //            playerInRange = col.gameObject;
    //}
    //void OnTriggerExit(Collider col)
    //{
    //    if (col.gameObject == playerInRange)
    //        playerInRange = null;
    //}

    void FixedUpdate ()
    {
        //state machine
        StateMachine();
        Debug.Log(currentState);

        //go to dead state from any state if health is less than 1
        if (health < 1)
        {
            Debug.Log(gameObject.name + " Dead");
            currentState = State.Dead;
        }

        Debug.Log(gameObject.name + " " + health);
    //    //Debug.Log(gameObject.name + " " + currentState);
    }

    ////calls when a trigger hit this object
    //void OnTriggerEnter (Collider col)
    //{
    //    //checking if it is a bullet
    //    if (col.tag == "Bullet")
    //    {
    //        currentState = State.TakeDmg;
    //        health -= 20;
    //    }
    //}
}
