using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Senior.Items
{
    public class DemonicEssence : Item
    {
        public Bomb DemonicBomb;

        public override void OnHit(Entity target, float damage)
        {
            Bomb bomb = Instantiate(DemonicBomb, target.transform.position, Quaternion.identity) as Bomb;
            bomb.Initialize(owner, owner.enemyFactions);
            OnUse();

        }
    }
}