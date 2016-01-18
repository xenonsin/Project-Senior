using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
using Seniors.Skills.Buffs;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Seniors.Skills
{
    public class KenoSkill1 : Skill
    {
        public GameObject SparksPrefab;
        public TempestShield TempestShieldPrefab;

        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;


            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();
                anim.SetTrigger("Skill1");
            }
        }

        public override void RaiseEvent(string eventName)
        {
            switch (eventName)
            {
                case "Skill1_Sparks":
                    Instantiate(SparksPrefab, hero.transform.position, Quaternion.identity);
                    break;
                case "Skill1_Shield":
                    TempestShield shield = Instantiate(TempestShieldPrefab, hero.transform.position, Quaternion.identity) as TempestShield;
                    shield.Initialize(hero,hero);
                    break;
            }
        }
    }
}