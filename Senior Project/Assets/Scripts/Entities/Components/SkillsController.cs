using System.Collections.Generic;
using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
using Seniors.Skills;
using UnityEngine;

namespace Senior.Components
{
    public class SkillsController : MonoBehaviour
    {
        public List<Skill> HeroSkills = new List<Skill>();

        public void Initialize(HeroController hc, Hero hero, Animator anim, Rigidbody rb)
        {
            if (HeroSkills != null)
            {
                for (int i = 0; i < HeroSkills.Count; i++)
                {
                    HeroSkills[i].Initialize(hc, hero, anim, rb);
                }
            }
        }

        public void Attack()
        {
            if (HeroSkills[0] != null)
                HeroSkills[0].Activate();
        }

        public void AltAttack()
        {
            if (HeroSkills[1] != null)
                HeroSkills[1].Activate();
        }

        public void SkillOne()
        {
            if (HeroSkills[2] != null)
                HeroSkills[2].Activate();
        }

        public void SkillTwo()
        {
            if (HeroSkills[3] != null)
                HeroSkills[3].Activate();
        }

        public void SkillThree()
        {
            if (HeroSkills[4] != null)
                HeroSkills[4].Activate();
        }

        public void SkillFour()
        {
            if (HeroSkills[5] != null)
                HeroSkills[5].Activate();
        }

        public void RaiseEvent(string eventName)
        {
            for (int i = 0; i < HeroSkills.Count; i++)
            {
                HeroSkills[i].RaiseEvent(eventName);
            }
        }
    }
}