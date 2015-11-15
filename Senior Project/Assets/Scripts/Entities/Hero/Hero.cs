using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    [RequireComponent(typeof(HeroController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Hero : MonoBehaviour
    {
        public int HealthMax = 100;
        public int HealthCurrent { get; private set; }

        public int StaminaMax = 100;
        public int StaminaCurrent { get; private set; }

        public int SpeedMovement = 5;
        public int SpeedRotation = 5;

        private Rigidbody rb;

        public void Awake()
        {
            HealthCurrent = HealthMax;
            StaminaCurrent = StaminaMax;
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
}