using UnityEngine;
using System.Collections;


public class MonsterStatus : MonoBehaviour {

    private GameObject aggroRange;    
    private Animation anim;
    private Vector3 spawnLocation;
    private float rotateSpeed = 10f;
    private float movespeed = 0.5f;
    public GameObject playerInRange;
    public float aggroRadius = 2f;
    public float distanceToPlayer;
    private float meleeRange = 0.7f;
    public float maxHealth = 100f;
    public float currentHealth;

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
            case State.Attack:
                atk_state();
                break;
            case State.TakeDmg:
                dmg_state();
                break;
            case State.Dead:
                dead_state();
                break;
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

        //if player is out of aggro range, go back to idle
        if (playerInRange == null)
            currentState = State.Idle;
        else
        {
            //look at the targeted player
            Vector3 pos = playerInRange.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            //move towards the targeted player and switch to attack
            if (distanceToPlayer > meleeRange)
                transform.position = Vector3.MoveTowards(transform.position, playerInRange.transform.position, movespeed * Time.deltaTime);
            else
                currentState = State.Attack;
        }
    }

    private bool attackAnimation = false;

    void atk_state()
    {
        //attack once
        if (!attackAnimation)
        {
            anim["Attack"].speed = 0.5f;
            anim.Play("Attack");
            attackAnimation = true;
        }

        //if player is out of melee range, go to run state
        if (distanceToPlayer > meleeRange)
            currentState = State.Run;

        //reset attackanimation
        if(!anim.IsPlaying("Attack"))
            attackAnimation = false;
    }

    private bool dmgAnimation = false;

    void dmg_state()
    {
        if (!dmgAnimation)
        {
            anim.Play("TakeDmg");
            dmgAnimation = true;
        }
        if (!anim.IsPlaying("TakeDmg"))
        {
            currentState = State.Idle;
            dmgAnimation = false;
        }
    }

    private bool deadAnimation = false;

    void dead_state()
    {
        //play dead animation once
        if (!deadAnimation)
        {
            anim["Die"].speed = 0.5f;
            anim.Play("Die");
            deadAnimation = true;
        }

        //destroy gameobject after dead animation is finished
        if (!anim.IsPlaying("Die"))
            Destroy(gameObject);
    }

    void Awake()
    {
        //initializing healthbar
        currentHealth = maxHealth;
        backgroundHealthBar.SetPixel(0, 0, Color.black);
        backgroundHealthBar.Apply();
        foregroundHealthBar.SetPixel(0, 0, Color.red);
        foregroundHealthBar.Apply();

        //initializing animation component
        anim = GetComponent<Animation>();

        //initializing aggroDetector
        aggroRange = new GameObject("aggroRange");
        aggroRange.transform.position = gameObject.transform.position;
        aggroRange.transform.SetParent(gameObject.transform);
        aggroRange.AddComponent<SphereCollider>();
        aggroRange.GetComponent<SphereCollider>().isTrigger = true;
        aggroRange.GetComponent<SphereCollider>().radius = aggroRadius;
    }

    void FixedUpdate ()
    {
        //state machine
        StateMachine();
        Debug.Log(currentState);

        if (playerInRange!=null)
            distanceToPlayer = Mathf.Sqrt((Mathf.Abs(gameObject.transform.position.x - playerInRange.transform.position.x))
                                        + (Mathf.Abs(gameObject.transform.position.z - playerInRange.transform.position.z)));

        //go to dead state from any state if health is less than 1
        if (currentHealth < 1)
        {
            Debug.Log(gameObject.name + " Dead");
            currentState = State.Dead;
        }

        Debug.Log(gameObject.name + " " + currentHealth);

        //healthbar GUI
        healthbarGUI();
    }

    //aggroDetector Behaviors - Follows one person forever
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            if (playerInRange == null)
                playerInRange = col.gameObject;
    }

    //calls when a trigger hit this object
    void OnCollisionEnter(Collision col)
    {
        //checking if it is a bullet
        if (col.gameObject.tag == "Bullet")
        {
            currentState = State.TakeDmg;
            currentHealth -= 20f;
        }
    }


    //healthbar GUI
    private Texture2D backgroundHealthBar = new Texture2D(1, 1);
    private Texture2D foregroundHealthBar = new Texture2D(1, 1);
    private float barLeft, barTop, barWidth = 30f, barHeight = 5f;
    private Vector2 barPos;
    private float healthPercent;

    void healthbarGUI()
    {
        barPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        barLeft = barPos.x - 10f;
        barTop = (Screen.height - barPos.y) - 30f;
        healthPercent = currentHealth / maxHealth;
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(barLeft, barTop, barWidth, barHeight), backgroundHealthBar);
        GUI.DrawTexture(new Rect(barLeft, barTop, barWidth * healthPercent, barHeight), foregroundHealthBar);
    }

}
