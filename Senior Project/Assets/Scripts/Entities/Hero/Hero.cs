
using Senior.Components;
using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    
    public class Hero : Entitiy
    {
        public Inventory InventoryComponent { get; set; }           // Contains the inventory of the character
        public SkillsController SkillsComponent { get; set; }       // Listens to the player input to cast skills

        public override void Awake()
        {
            base.Awake();
            InventoryComponent = GetComponentInChildren<Inventory>();
            SkillsComponent = GetComponentInChildren<SkillsController>();
        }

        void Update()        
        {
            //replace with an event that listens whenever the health is changed.
            //if (StatsComponent.HealthCurrent <= 0)
               // Dead();
        }

        
    
    }
}