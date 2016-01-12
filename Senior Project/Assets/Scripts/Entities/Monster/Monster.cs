using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Monster
{
    public class Monster : Entitiy
    {
         //Monster Controller
         //AI which keep track of states
         //UI which shows health bar

        public Image HealthFill;

        public override void Damage(int damage)
        {
            base.Damage(damage);
            Debug.Log(name + " has been hit for " + damage + " damage!");
            HealthFill.fillAmount = StatsComponent.HealthCurrent/(float)StatsComponent.HealthMax;
        }
    }
}