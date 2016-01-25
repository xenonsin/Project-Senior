using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Andrew
{
    public class AndrewSkill3 : Skill
    {
        public LightningEssence lightingEssensePrefab;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();

                LightningEssence fe = Instantiate(lightingEssensePrefab, owner.transform.position, Quaternion.identity) as LightningEssence;
                fe.Initialize(owner);
            }
        }
    }
}