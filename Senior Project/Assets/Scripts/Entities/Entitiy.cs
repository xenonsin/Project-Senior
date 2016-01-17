using Senior.Components;
using Senior.Globals;
using Senior.Inputs;
using Senior.Managers;
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
        public bool showDamagePopup;
        public WorldUI damagePrefab;


        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
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
                    Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

                    WorldUI damageGO = Instantiate(damagePrefab, screenPoint, Quaternion.identity) as WorldUI;
                    damageGO.gameObject.SetActive(true);
                    damageGO.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
                    damageGO.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

                    damageGO.gameObject.GetComponentInChildren<Text>().text = damage.ToString();

                }
            }

            if (StatsComponent.HealthCurrent <= 0)
                Die();
        }

        // Similar to the damaged method, but gets it's own method for ease of use.
        public virtual void Heal(int heal)
        {
            if (StatsComponent.HealthCurrent + heal > StatsComponent.HealthMax)
                StatsComponent.HealthCurrent = StatsComponent.HealthMax;
            else
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