using Senior.Components;
using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public abstract class Entitiy : MonoBehaviour
    {

        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        private Rigidbody rb;

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            StatsComponent = GetComponent<Stats>();
            FullHeal();
            StatsComponent.HealthModifier = 0;
        }


        // Called when the entity dies
        public virtual void Die()
        {
            
        }

        // Called when the entity gets damaged
        public virtual void GetDamaged(int damage)
        {
            StatsComponent.HealthCurrent -= damage;

            if (StatsComponent.HealthCurrent <= 0)
                Die();
        }

        // Similar to the damaged method, but gets it's own method for ease of use.
        public virtual void GetHealed(int heal)
        {
            StatsComponent.HealthCurrent += heal;
        }

        // Called when you want the entity to get fully healed.
        public virtual void FullHeal()
        {
            StatsComponent.HealthCurrent = StatsComponent.HealthMax;
        }

        public virtual void Update()
        {
            
        }


    }
}