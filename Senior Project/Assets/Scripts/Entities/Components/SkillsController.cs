using System.Collections.Generic;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Components;
using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
using Seniors.Skills;
using UnityEngine;

namespace Senior.Components
{
    public class SkillsController : MonoBehaviour
    {
        public List<Skill> HeroSkills = new List<Skill>();
        public bool IsBusy = false;
        public void Initialize(IMovementController hc, Entity hero, Animator anim, Rigidbody rb)
        {
            if (HeroSkills != null)
            {
                for (int i = 0; i < HeroSkills.Count; i++)
                {
                    HeroSkills[i].Initialize(this,hc, hero, anim, rb);
                }
            }
        }

        public void AttackDown()
        {
            if (HeroSkills[0] != null)
                HeroSkills[0].ActivateDown();
        }

        public void AttackUp()
        {
            if (HeroSkills[0] != null)
                HeroSkills[0].ActivateUp();
        }

        public void AltAttackDown()
        {
            if (HeroSkills[1] != null)
                HeroSkills[1].ActivateDown();
        }
        public void AltAttackUp()
        {
            if (HeroSkills[1] != null)
                HeroSkills[1].ActivateUp();
        }

        public void SkillOneDown()
        {
            if (HeroSkills[2] != null)
                HeroSkills[2].ActivateDown();
        }
        public void SkillOneUp()
        {
            if (HeroSkills[2] != null)
                HeroSkills[2].ActivateUp();
        }

        public void SkillTwoDown()
        {
            if (HeroSkills[3] != null)
                HeroSkills[3].ActivateDown();
        }
        public void SkillTwoUp()
        {
            if (HeroSkills[3] != null)
                HeroSkills[3].ActivateUp();
        }
        public void SkillThreeDown()
        {
            if (HeroSkills[4] != null)
                HeroSkills[4].ActivateDown();
        }
        public void SkillThreeUp()
        {
            if (HeroSkills[4] != null)
                HeroSkills[4].ActivateUp();
        }
        public void SkillFourDown()
        {
            if (HeroSkills[5] != null)
                HeroSkills[5].ActivateDown();
        }
        public void SkillFourUp()
        {
            if (HeroSkills[5] != null)
                HeroSkills[5].ActivateUp();
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