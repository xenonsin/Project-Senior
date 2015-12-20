
using Senior;
using Senior.Components;
using Senior.Inputs;
using Senior.Items;
using UnityEngine;

namespace Assets.Scripts.Entities.Hero
{
    [RequireComponent(typeof(HeroController))]

    public class Hero : Entitiy
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
            if (Input.anyKeyDown)
                Damage(20);
            base.Update();
        }

        public override void Die()
        {  
            //Turn into spirit          
            if (owner)
                owner.OnDead(this);
        }

        public override void Damage(int damage)
        {
            base.Damage(damage);
            if (owner)
                owner.OnHealthModified(this);
        }

        public override void Heal(int heal)
        {
            base.Heal(heal);
            if (owner)
                owner.OnHealthModified(this);

        }

        public override void FullHeal()
        {
            base.FullHeal();

            if (owner)
                owner.OnHealthModified(this);
        }

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

        public void PickUpItem(Item item)
        {
            item.transform.parent = InventoryComponent.transform;

            if (owner)
                owner.OnItemPickUp(item);
        }


    }
}