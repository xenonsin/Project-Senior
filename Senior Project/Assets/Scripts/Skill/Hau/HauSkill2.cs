using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Hau
{
    public class HauSkill2 : Skill
    {
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                anim.SetTrigger("Skill2");
            }
        }
        public override void RaiseEvent(string eventName)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Healing Bomb")) return;

            switch (eventName)
            {
                case "TossBomb":
                    ShootProjectile();
                    break;
            }
        }
    }
}