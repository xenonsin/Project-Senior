using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Components
{
    public class Stats : MonoBehaviour
    {
        public int Experience { get; set; }
        public int Level { get; set; }

        public int HealthBase = 100;
        public int HealthCurrent { get; private set; }

        public int StaminaBase = 100;
        public int StaminaCurrent { get; private set; }

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
            HealthCurrent = HealthBase;
            StaminaCurrent = StaminaBase;
        }


    }
}