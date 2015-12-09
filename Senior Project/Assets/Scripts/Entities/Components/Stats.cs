using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Components
{
    public class Stats : MonoBehaviour
    {
        public int Experience { get; set; }
        public int Level { get; set; }

        // The base health granted to the entity
        public int HealthBase = 100;
        public int HealthCurrent { get; set; }
        // The health granted per level, current default value is 32 (set in the editor to modify)
        public int HealthPerLevel = 32;
        // The sum of all health modifications due to items/skills/buffs/debuffs
        public int HealthModifier { get; set; }
        //The max health as the result of the formula
        public int HealthMax
        {
            get { return (HealthBase + HealthPerLevel * Level + HealthModifier); }
        }

        public int StaminaBase = 100;
        public int StaminaCurrent { get; set; }

        public int AttackSpeedBase = 5;
        public int AttackSpeedCurrent { get; set; }

        public int DamageBase = 5;
        public int DamageCurrent { get; set; }

        public int MovementSpeedBase = 5;
        public int MovementSpeedCurrent { get; set; }

        public int RotationSpeedBase = 20;
        public int RotationSpeedCurrent { get; set; }

        public void Awake()
        {
            //HealthCurrent = HealthBase;
            //StaminaCurrent = StaminaBase;
        }


    }
}