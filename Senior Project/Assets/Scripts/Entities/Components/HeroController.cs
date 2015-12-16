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

        public void Start()
        {
            playerController = GetComponentInParent<IPlayerController>();
            skills = GetComponentInChildren<SkillsController>();

            stats = GetComponent<Stats>();
            rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            if (playerController.AttackButton)
                skills.Attack();
            if (playerController.AltAttackButton)
                skills.AltAttack();
            if (playerController.SkillOneButton)
                skills.SkillOne();
            if(playerController.SkillTwoButton)
                skills.SkillTwo();
            if(playerController.SkillThreeButton)
                skills.SkillThree();
            if(playerController.SkillFourButton)
                skills.SkillFour();
        }

        public void FixedUpdate()
        {
            Move();      
        }

        public void Move()
        {
            var moveDirection = new Vector3(playerController.MoveInput.x, 0, playerController.MoveInput.y);
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0;

            //Makes sure the player faces the direction they're moving.
            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * stats.RotationSpeedBase);

            rb.MovePosition(GetComponent<Rigidbody>().position + moveDirection.normalized * stats.MovementSpeedBase * Time.deltaTime);
        }
    }
}