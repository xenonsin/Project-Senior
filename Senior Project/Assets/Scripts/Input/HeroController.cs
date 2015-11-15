using Assets.Scripts.Entities.Hero;
using UnityEngine;
using UnityEngine.Networking;

namespace Senior.Inputs
{
    [RequireComponent(typeof(Hero))]
    [RequireComponent(typeof(Rigidbody))]
    public class HeroController : MonoBehaviour
    {
        private Hero hero;
        private IPlayerController playerController;
        private Rigidbody rb;

        public void Start()
        {
            playerController = GetComponentInParent<IPlayerController>();
            hero = GetComponent<Hero>();
            rb = GetComponent<Rigidbody>();
        }

        public void Update()
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
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * hero.SpeedRotation);

            rb.MovePosition(GetComponent<Rigidbody>().position + moveDirection.normalized * hero.SpeedMovement * Time.deltaTime);
        }
    }
}