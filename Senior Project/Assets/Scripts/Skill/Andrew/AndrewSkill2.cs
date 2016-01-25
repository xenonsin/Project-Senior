using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Andrew
{
    public class AndrewSkill2 : Skill
    {
        public IceEssence iceEssensePrefab;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();

                IceEssence fe = Instantiate(iceEssensePrefab, owner.transform.position, Quaternion.identity) as IceEssence;
                fe.Initialize(owner);
            }
        }
    }
}