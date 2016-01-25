using UnityEngine.UI;

namespace Seniors.Skills.Buffs
{
    public class StunDebuff : Buff
    {
        public override void OnAdd()
        {
            target.mc.CanMove = false;
        }

        public override void OnDisable()
        {
            target.mc.CanMove = true;
            base.OnDisable();
        }
    }
}