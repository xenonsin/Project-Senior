using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Senior.Items
{
    public class DemonicEssence : Item
    {
        public Buff demonicPrefab;
        public GameObject demonicEssencePrefab;

        public override void OnEquip()
        {
            //spawn fire essence prefab that circles around the player
            Debug.Log("on equpo");
        }

        public override void OnHit(Entity target, int damage)
        {
            // give the target the burn debuff
            Debug.Log("i quiep");
            OnUse();
        }
    }
}