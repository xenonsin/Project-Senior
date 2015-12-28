﻿using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
using UnityEngine;

namespace Seniors.Skills
{
    public class KenoSkill3 : Skill
    {
        public override void Activate()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;
            if (!IsDisabled)
            {
                IsDisabled = true;
                OnCast();
                anim.SetTrigger("Skill3");
            }
        }
    }
}