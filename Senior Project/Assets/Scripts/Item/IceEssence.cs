using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Senior.Items
{
    public class IceEssence : Item
    {
        public Buff icePrefab;
        public GameObject iceEssencePrefab;

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