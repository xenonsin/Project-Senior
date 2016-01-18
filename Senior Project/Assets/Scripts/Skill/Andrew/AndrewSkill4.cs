using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Andrew
{
    public class AndrewSkill4 : Skill
    {
        public DemonicEssence demonicEssensePrefab;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();

                DemonicEssence fe = Instantiate(demonicEssensePrefab, hero.transform.position, Quaternion.identity) as DemonicEssence;
                fe.Initialize(hero);
            }
        }
    }
}