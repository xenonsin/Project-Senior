using Assets.Scripts.Entities.Hero;
using Senior.Components;
using UnityEngine;

namespace Senior.Inputs
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Rigidbody))]
    public class HeroController : MonoBehaviour
    {
        private Stats stats;
        private IPlayerController playerController;
        private SkillsController skills;
        private Rigidbody rb;
        private Animator anim;
        private Hero hero;

        public bool CanMove { get; set; }

        private Vector3 lastMoveDirection;

        public Vector3 LastMoveDirection
        {
            get {return lastMoveDirection;}
            set { lastMoveDirection = value.normalized; }
        }

        public void Start()
        {
            hero = GetComponent<Hero>();
            playerController = GetComponentInParent<IPlayerController>();
            skills = GetComponentInChildren<SkillsController>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            stats = GetComponent<Stats>();

            skills.Initialize(this, hero, anim, rb);
            CanMove = true;

        }

        public void Update()
        {
            if (playerController != null)
            {
                if (playerController.AttackButton)
                    skills.Attack();
                if (playerController.AltAttackButton)
                    skills.AltAttack();
                if (playerController.SkillOneButton)
                    skills.SkillOne();
                if (playerController.SkillTwoButton)
                    skills.SkillTwo();
                if (playerController.SkillThreeButton)
                    skills.SkillThree();
                if (playerController.SkillFourButton)
                    skills.SkillFour();
            }
        }

        public void FixedUpdate()
        {
            if (playerController != null && CanMove)
                Move();      
        }

        public void Move()
        {

            var moveDirection = new Vector3(playerController.MoveInput.x, 0, playerController.MoveInput.y);
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;

            if (anim != null)
            {
                anim.SetFloat("Speed", moveDirection.normalized.sqrMagnitude);
            }

            //Makes sure the player faces the direction they're moving.
            if (moveDirection != Vector3.zero)
            {
                LastMoveDirection = moveDirection;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection),
                    Time.deltaTime*stats.RotationSpeedBase);
            }
            rb.MovePosition(rb.position + moveDirection.normalized * stats.MovementSpeedBase * Time.deltaTime);
        }

        public void LungeForward()
        {
            var moveDirection = new Vector3(playerController.MoveInput.x, 0, playerController.MoveInput.y);
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;


            //Makes sure the player faces the direction they're moving.
            if (moveDirection != Vector3.zero)
            {
                LastMoveDirection = moveDirection;
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }

            rb.AddForce(rb.position + LastMoveDirection * 20, ForceMode.Impulse);
        }

        public void EnableTransitions()
        {
            anim.SetBool("DisableTransitions", false);
        }
    }
}