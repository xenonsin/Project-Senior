using System.Collections.Generic;
using Assets.Scripts.Entities;
using Senior.Items;
using UnityEngine;

namespace Senior.Components
{
    public class Inventory : MonoBehaviour
    {
         List<Item> items = new List<Item>();

        public void Equip(Item item)
        {
            if (!item.canOwnMultiple)
            {
                if (items.Contains(item))
                    return;
            }

            item.transform.SetParent(this.transform);
            item.OnEquip();
            items.Add(item);

        }

        public void UnEquip(Item item)
        {
            items.Remove(item);
        }

        public void OnHit(Entity target, int damage)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].OnHit(target, damage);
            }
        }

        public void OnDamage(Entity dealer, int damage)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].OnDamage(dealer, damage);
            }
        }
    }
}