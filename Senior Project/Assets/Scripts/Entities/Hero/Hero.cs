
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

        public override void Update()        
        {
            base.Update();
        }

        public override void GetDamaged(int damage)
        {
            base.GetDamaged(damage);
        }

        public override void GetHealed(int heal)
        {
            base.GetHealed(heal);
        }
    }
}