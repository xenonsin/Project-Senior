using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Seniors.Skills.Lung
{
    public class LungSkill2 : Skill
    {
        public Buff stealthBuff;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;


            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();
                anim.SetTrigger("Skill2");

            }

        }

        public override void RaiseEvent(string eventName)
        {
            switch (eventName)
            {
                case "Skill2_CastSmoke":
                    ShootProjectile();
                    break;
                case "Skill2_GoInvis":
                    Buff buff = Instantiate(stealthBuff, owner.transform.position, Quaternion.identity) as Buff;
                    buff.Initialize(owner,owner);
                    break;
            }
        }
    }
}