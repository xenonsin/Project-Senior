using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
using UnityEngine;

namespace Seniors.Skills
{
    public class KenoSkill2 : Skill
    {
        public override void Activate()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();
                anim.SetTrigger("Skill2");
                rb.AddForce(hc.LastMoveDirection * 50, ForceMode.Impulse);
            }
        }
    }
}