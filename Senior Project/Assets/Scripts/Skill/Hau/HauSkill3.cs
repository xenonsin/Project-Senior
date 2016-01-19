namespace Seniors.Skills.Hau
{
    public class HauSkill3 : Skill
    {
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                anim.SetTrigger("Skill3");
            }
        }
        public override void RaiseEvent(string eventName)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Ice Bomb")) return;

            switch (eventName)
            {
                case "TossBomb":
                    ShootProjectile();
                    break;
            }
        }
    }
}