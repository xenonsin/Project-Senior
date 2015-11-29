using Assets.Scripts.Entities.Hero;
using Senior.Components;
using UnityEngine;
using UnityEngine.Networking;

namespace Senior.Inputs
{
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Rigidbody))]
    public class HeroController : MonoBehaviour
    {
        private Stats stats;
        private IPlayerController playerController;
        private Rigidbody rb;

        public void Start()
        {
            playerController = GetComponentInParent<IPlayerController>();
            stats = GetComponent<Stats>();
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
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * stats.RotationSpeedBase);

            rb.MovePosition(GetComponent<Rigidbody>().position + moveDirection.normalized * stats.MovementSpeedBase * Time.deltaTime);
        }
    }
}