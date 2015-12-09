
using Senior;
using Senior.Components;
using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    [RequireComponent(typeof(HeroController))]

    public class Hero : Entitiy
    {
        public Inventory InventoryComponent { get; set; }           // Contains the inventory of the character
        public SkillsController SkillsComponent { get; set; }       // Listens to the player input to cast skills

        public Sprite Portrait;
        public Player owner;

        public override void Awake()
        {
            base.Awake();
            InventoryComponent = GetComponentInChildren<Inventory>();
            SkillsComponent = GetComponentInChildren<SkillsController>();
            StatsComponent.Level = 1;           
        }


        public override void Update()        
        {
            if (Input.anyKeyDown)
            {
                GetDamaged(12);
            }
            base.Update();
        }

        public override void GetDamaged(int damage)
        {
            base.GetDamaged(damage);
            if (owner)
                owner.OnHealthModified(this);
            

        }

        public override void GetHealed(int heal)
        {
            base.GetHealed(heal);
            if (owner)
                owner.OnHealthModified(this);

        }

        public override void FullHeal()
        {
            base.FullHeal();

            if (owner)
                owner.OnHealthModified(this);

        }
    }
}