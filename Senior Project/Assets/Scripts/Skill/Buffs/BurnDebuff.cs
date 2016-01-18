namespace Seniors.Skills.Buffs
{
    public class BurnDebuff : Buff
    {
        public int damage = 0;
        public override void OnTick()
        {
            target.Damage(owner, damage);
        }
    }
}