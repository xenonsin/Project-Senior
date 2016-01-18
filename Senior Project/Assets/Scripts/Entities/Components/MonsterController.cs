using UnityEngine;

namespace Assets.Scripts.Entities.Components
{
    public class MonsterController : MonoBehaviour, IMovementController
    {
        public bool RotateBasedOnMovement { get; set; }
        public bool OnlyRotate { get; set; }
        public bool CanMove { get; set; }
        public Vector3 MoveDirection { get; set; }
        public Vector3 LastMoveDirection { get; set; }
        public void Start()
        {
        }

        public void Move()
        {
        }

        public void FixedUpdate()
        {
        }

        public void Update()
        {
        }

        public void AnimationEvent(string eventName)
        {
        }
    }
}