using System.Collections;
using UnityEngine;

namespace Seniors.Skills
{
    public class SwingSword : Skill
    {
        private float comboRate = .2f;

        private bool attack;
        private bool toggle;
        public override void Activate()
        {
            anim.SetTrigger("Attack");
        }
    }
}