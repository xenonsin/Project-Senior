using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Andrew
{
    public class AndrewSkill1 : Skill
    {
        public FireEssence fireEssensePrefab;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();
                
                FireEssence fe = Instantiate(fireEssensePrefab, hero.transform.position, Quaternion.identity) as FireEssence;
                fe.Initialize(hero);
            }
        }
    }
}