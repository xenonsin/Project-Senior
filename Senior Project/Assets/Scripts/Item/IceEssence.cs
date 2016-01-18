using Assets.Scripts.Entities;
using Seniors.Skills;
using Seniors.Skills.Buffs;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Senior.Items
{
    public class IceEssence : Item
    {
        public Bomb IceBomb;

        public override void OnHit(Entity target, int damage)
        {
            Bomb bomb = Instantiate(IceBomb, target.transform.position, Quaternion.identity) as Bomb;
            bomb.owner = owner;
            OnUse();

        }
    }
}