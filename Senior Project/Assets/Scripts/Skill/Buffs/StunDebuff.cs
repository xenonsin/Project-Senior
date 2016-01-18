using UnityEngine.UI;

namespace Seniors.Skills.Buffs
{
    public class StunDebuff : Buff
    {
        public override void OnAdd()
        {
            target.mc.CanMove = false;
        }

        public override void OnDestroy()
        {
            target.mc.CanMove = true;
            base.OnDestroy();
        }
    }
}