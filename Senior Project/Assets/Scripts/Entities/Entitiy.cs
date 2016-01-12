using Senior.Components;
using Senior.Globals;
using Senior.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public abstract class Entitiy : MonoBehaviour
    {
        public string name;
        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        private Rigidbody rb;
        public Faction currentFaction;
        public Faction alliedFactions;
        public Faction enemyFactions;

        [Header("WorldUI")]
        public Canvas personalCanvas;
        public bool showDamagePopup;
        public GameObject damagePrefab;


        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            StatsComponent = GetComponent<Stats>();
            StatsComponent.HealthModifier = 0;


        }

        public virtual void Start()
        {
            FullHeal();
        }


        // Called when the entity dies
        public virtual void Die()
        {

        }

        // Called when the entity gets damaged
        public virtual void Damage(int damage)
        {
            StatsComponent.HealthCurrent -= damage;

            if (showDamagePopup)
            {
                if (damagePrefab != null)
                {
                    GameObject damageGO = Instantiate(damagePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    damageGO.GetComponent<RectTransform>().SetParent(personalCanvas.transform);
                    damageGO.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                    damageGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,1);

                    damageGO.GetComponentInChildren<Text>().text = damage.ToString();

                }
            }

            if (StatsComponent.HealthCurrent <= 0)
                Die();
        }

        // Similar to the damaged method, but gets it's own method for ease of use.
        public virtual void Heal(int heal)
        {
            StatsComponent.HealthCurrent += heal;
        }

        // Called when you want the entity to get fully healed.
        public virtual void FullHeal()
        {
            StatsComponent.HealthCurrent = StatsComponent.HealthMax;
        }

        public virtual void Update()
        {

        }

        public virtual void OnCollisionEnter(Collision collision)
        {

        }
    }
}