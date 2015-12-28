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
            for (int i = 0; i < HeroSkills.Count; i++)
            {
                HeroSkills[i].Initialize(hc, hero, anim, rb);
            }
        }

        public void Attack()
        {
            HeroSkills[0].Activate();
        }

        public void AltAttack()
        {
            HeroSkills[1].Activate();
        }

        public void SkillOne()
        {
            HeroSkills[2].Activate();

        }

        public void SkillTwo()
        {
            HeroSkills[3].Activate();

        }

        public void SkillThree()
        {
            HeroSkills[4].Activate();

        }

        public void SkillFour()
        {
            HeroSkills[5].Activate();

        }
    }
}