
using Senior;
using Senior.Components;
using Senior.Inputs;
using Senior.Items;
using Seniors.Skills;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    [RequireComponent(typeof(HeroController))]

    public class Hero : Entity
    {
        public Inventory InventoryComponent { get; set; }           // Contains the inventory of the character
        public Sprite Portrait;
        public Player owner;

        public void Initialize(Player player)
        {
            owner = player;
        }

        public override void Awake()
        {
            base.Awake();
            InventoryComponent = GetComponentInChildren<Inventory>();
            StatsComponent.Level = 1;           
        }


        public override void Update()        
        {
            base.Update();
        }

        // the hero is dead
        public override void Die()
        {  
            //Turn into spirit          
            if (owner)
                owner.OnDead(this);
        }

        // the hero is damaged by a certain amount
        public override void Damage(Entity dealer, int damage)
        {
            base.Damage(dealer,damage);
            InventoryComponent.OnDamage(dealer, damage);
            if (owner)
                owner.OnHealthModified(this);
        }

        // the hero is healed by a certain amount
        public override void Heal(int heal)
        {
            base.Heal(heal);
            if (owner)
                owner.OnHealthModified(this);

        }

        // the hero is fully healed
        public override void FullHeal()
        {
            base.FullHeal();

            if (owner)
                owner.OnHealthModified(this);
        }

        // the hero collides with an object
        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            //Pick up Items automatically when you touch them
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                PickUpItem(item);
            }
        }

        // the hero picks up an item
        public void PickUpItem(Item item)
        {
            item.transform.parent = InventoryComponent.transform;

            if (owner)
                owner.OnItemPickUp(item);
        }

        // Is used to notify the UI that a skill has been used, and the cd should start
        public void UseSkill(Skill skill)
        {
            if (skill.showInUi)
                owner.UseSkill(skill);
        }

        // Is used to update the skill cd for the ui
        public void UpdateSkill(Skill skill)
        {
            owner.UpdateSkill(skill);
        }

        // called when a skill has hit an enemy, incokes any OnHit events ex. from items
        public void OnHit(Entity entitiy, int damage)
        {
            InventoryComponent.OnHit(entitiy,damage);
            
        }

        // called when casting a skill, invokes and OnCast events ex. from items
        public void OnCast()
        {
            
        }
    }
}