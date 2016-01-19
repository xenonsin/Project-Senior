namespace Seniors.Skills.Buffs
{
    public class LifeLeachDebuff : Buff
    {
        public int damage = 0;
        public override void OnTick()
        {
            target.Damage(owner, damage);
            owner.Heal(damage);
        }
    }
}