using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Hero;
using Seniors.Skills;
using UnityEngine;

namespace Senior.Items
{
    public abstract class Item : MonoBehaviour
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public Hero owner;
        public bool limitedUses = false;
        public int numberOfUses = 1;
        public bool limitedTime = false;
        public float lifespan = 1;
        public bool canOwnMultiple = true;
        public bool hasDurability = false;
        public int healthMax = 100;
        public int healthCurrent;

        // sets the lifespan of the item
        public virtual void Start()
        {
            healthCurrent = healthMax;
            if (limitedTime)
                Destroy(gameObject, lifespan);
        }

        public virtual void Initialize(Hero owner)
        {
            this.owner = owner;
            this.owner.InventoryComponent.Equip(this);
            OnEquip();
        }

        // when the item is initially equipped
        public virtual void OnEquip()
        {
            
        }

        // when the player hits a target
        public virtual void OnHit(Entity target, int damage)
        {
        }

        // When the player got damaged
        public virtual void OnDamage(Entity dealer, int damage)
        {
            if (hasDurability)
            {
                healthCurrent -= damage;

                if (healthCurrent <= 0)
                    Destroy(gameObject);
            }
        }

        // when the player casts a spell
        public virtual void OnCast(Skill skill)
        {
            
        }

        public virtual void Repair()
        {
            healthCurrent = healthMax;
        }

        // when the item is used
        public virtual void OnUse()
        {
            if (limitedUses)
            {
                numberOfUses -= 1;
                if (numberOfUses <= 0)
                    Destroy(gameObject);
            }
        }

        // when the item is destroyed, call to unquip it
        public virtual void OnDestroy()
        {
            if (owner)
                owner.InventoryComponent.UnEquip(this);
        }
    }
}