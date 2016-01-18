using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Seniors.Skills.Projectiles
{
    public class Bomb : Projectile
    {
        public Buff BuffToBeApplied;
        public override void OnHit(Entity target)
        {
            base.OnHit(target);

            if (BuffToBeApplied != null)
            {
                Buff buff = Instantiate(BuffToBeApplied, target.transform.position, Quaternion.identity) as Buff;
                buff.Initialize(owner,target);
                target.BuffManager.AddBuff(buff);
            }

        }
    }
}