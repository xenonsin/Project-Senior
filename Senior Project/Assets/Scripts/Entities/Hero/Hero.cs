
using Senior.Components;
using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    [RequireComponent(typeof(HeroController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public class Hero : MonoBehaviour, IEntitiy
    {
        private Rigidbody rb;
        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        public Inventory InventoryComponent { get; set; }           // Contains the inventory of the character
        public SkillsController SkillsComponent { get; set; }       // Listens to the player input to cast skills

        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            StatsComponent = GetComponent<Stats>();
            InventoryComponent = GetComponentInChildren<Inventory>();
            SkillsComponent = GetComponentInChildren<SkillsController>();
        }

        void Update()        
        {
            if (StatsComponent.HealthCurrent <= 0)
                Dead();
        }

        private void Dead()
        {
        }
    
    }
}