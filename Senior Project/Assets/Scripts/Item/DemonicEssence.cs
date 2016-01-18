using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Senior.Items
{
    public class DemonicEssence : Item
    {
        public GameObject demonichitEffect;
        public GameObject demonicEssencePrefab;
        public override void OnHit(Entity target, int damage)
        {
            // give the target the burn debuff
            // spawn hi effect
            OnUse();

        }
    }
}