using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Senior.Items
{
    public class LightningEssence : Item
    {
        public GameObject lightningHitEffect;

        public GameObject lightningEssencePrefab;

        public override void OnHit(Entity target, int damage)
        {
            // give the target the burn debuff
            OnUse();

        }
    }
}