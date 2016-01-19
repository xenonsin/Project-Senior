﻿using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Senior.Items
{
    public class FireEssence : Item
    {
        public Bomb FireBomb;

        public override void OnHit(Entity target, float damage)
        {
            Bomb bomb = Instantiate(FireBomb, target.transform.position, Quaternion.identity) as Bomb;
            bomb.Initialize(owner, owner.enemyFactions);
            OnUse();

        }
    }
}