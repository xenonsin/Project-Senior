namespace Seniors.Skills.Hau
{
    public class HauSkill4 : Skill
    {
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            if (!IsDisabled)
            {
                IsDisabled = true;
                anim.SetTrigger("Skill4");
            }
        }
        public override void RaiseEvent(string eventName)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Fire Bomb")) return;

            switch (eventName)
            {
                case "TossBomb":
                    ShootProjectile();
                    break;
            }
        }
    }
}