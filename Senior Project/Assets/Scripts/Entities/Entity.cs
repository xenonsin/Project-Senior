using Senior.Components;
using Senior.Globals;
using Senior.Inputs;
using Senior.Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public abstract class Entity : MonoBehaviour
    {
        public string name;
        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        private Rigidbody rb;
        private Animator anim;
        public Faction currentFaction;
        public Faction alliedFactions;
        public Faction enemyFactions;
        public GameObject hitEffect;
        public Vector3 hitEffectOffset;
        public Material hitMaterial;
        public SkinnedMeshRenderer renderer;
        private Material defaultMaterial;
        [Header("WorldUI")]
        public bool showDamagePopup;
        public WorldUI damagePrefab;
        public ChannelBarWorldUI channelBarPrefab;
        public GameObject channelBarGO { get; set; }
        public Image channelFill { get; set; }


        public virtual void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            renderer.GetComponentInChildren<SkinnedMeshRenderer>();
            if (renderer != null)
                defaultMaterial = renderer.material;
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
            StatsComponent = GetComponent<Stats>();
            StatsComponent.HealthModifier = 0;


        }

        public virtual void Start()
        {
            FullHeal();

            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

            // Setup Channel World UI
            ChannelBarWorldUI channel = Instantiate(channelBarPrefab, screenPoint, Quaternion.identity) as ChannelBarWorldUI;
            channelBarGO = channel.gameObject;
            channelBarGO.SetActive(true);
            channel.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
            channel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            channel.offset = new Vector2(0, -20);
            channel.owner = this;
            channelFill = channel.ChannelFill;
            channelBarGO.SetActive(false);
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
                //spawn hit effect
                if (hitEffect != null)
                {
                    GameObject hit = Instantiate(hitEffect, transform.position + hitEffectOffset, Quaternion.identity) as GameObject;
                }
                if (damagePrefab != null)
                {
                    //spawn damage number indicator
                    Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
                    WorldUI damageGO = Instantiate(damagePrefab, screenPoint, Quaternion.identity) as WorldUI;
                    damageGO.gameObject.SetActive(true);
                    damageGO.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
                    damageGO.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

                    damageGO.gameObject.GetComponentInChildren<Text>().text = damage.ToString();

                }
                if (renderer != null && anim != null)
                    StartCoroutine(FlashRedOnHit());
            }

            if (StatsComponent.HealthCurrent <= 0)
                Die();
        }

        //Flash Red and FreezeFrame
        private IEnumerator FlashRedOnHit()
        {
            renderer.material = hitMaterial;
            anim.enabled = false;
            yield return new WaitForSeconds(0.1f);
            renderer.material = defaultMaterial;
            anim.enabled = true;
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