namespace Seniors.Skills
{
    public class KenoAltAttack : Skill
    {
        public override void Activate()
        {
            anim.SetTrigger("AltAttack");
        }
    }
}