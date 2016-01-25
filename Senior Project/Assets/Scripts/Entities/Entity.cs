﻿using Senior.Components;
using Senior.Globals;
using Senior.Inputs;
using Senior.Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Entities.Components;
using Senior;
using Senior.Items;
using Seniors.Skills;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public abstract class Entity : MonoBehaviour
    {
        public string name;
        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        public BuffsManager BuffManager { get; set; }
        public IMovementController mc { get; set; }
        private Rigidbody rb;
        private Animator anim;
        [HideInInspector]
        public Faction currentFaction;
        [HideInInspector]
        public Faction alliedFactions;
        [HideInInspector]
        public Faction enemyFactions;
        public GameObject healEffect;
        public Vector3 healEffectOffset;
        public GameObject hitEffect;
        public Vector3 hitEffectOffset;
        public Material hitMaterial;
        public Material invisMaterial;
        public Renderer[] renderer;
        private List<Material> defaultMaterial = new List<Material>();
        [Header("WorldUI")]
        public bool showDamagePopup;
        public GameObject damagePrefab;
        public GameObject channelBarPrefab;
        public GameObject channelBarGO { get; set; }
        public Image channelFill { get; set; }
        public Inventory InventoryComponent { get; set; }

        public CapsuleCollider collider;
        public Player playerOwner;
        public Entity entityOwner;
        public bool IsInvis = false;

        public virtual void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            InventoryComponent = GetComponentInChildren<Inventory>();

            BuffManager = GetComponentInChildren<BuffsManager>();
            renderer = GetComponentsInChildren<Renderer>();
            collider = GetComponent<CapsuleCollider>();
            if (renderer != null)
            {
                for (int i = 0; i < renderer.Length; i++)
                {
                    defaultMaterial.Add(renderer[i].material); 
                }

            }
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

            if (channelBarPrefab != null)
            {
                // Setup Channel World UI
                channelBarGO = TrashMan.spawn(channelBarPrefab, screenPoint, Quaternion.identity);               
                channelBarGO.SetActive(true);
                ChannelBarWorldUI channel = channelBarGO.GetComponent<ChannelBarWorldUI>();
                if (channel != null)
                {
                    channel.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
                    channel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    channel.offset = new Vector2(0, -20);
                    channel.owner = this;
                    channelFill = channel.ChannelFill;
                    channelBarGO.SetActive(false);
                }
            }
        }


        // Called when the entity dies
        public virtual void Die()
        {

        }

        // Called when the entity gets damaged
        public virtual void Damage(Entity dealer, float damage)
        {
            StatsComponent.HealthCurrent -= damage;

            if (showDamagePopup)
            {
                //spawn hit effect
                if (hitEffect != null)
                {
                    
                    TrashMan.spawn(hitEffect, transform.position + hitEffectOffset, Quaternion.identity);
                }
                if (damagePrefab != null)
                {
                    //spawn damage number indicator
                    Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
                    var damageGO = TrashMan.spawn(damagePrefab, screenPoint, Quaternion.identity);
                    damageGO.gameObject.SetActive(true);
                    damageGO.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
                    damageGO.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

                    damageGO.gameObject.GetComponentInChildren<Text>().text = damage.ToString();
                    damageGO.gameObject.GetComponentInChildren<Text>().color = Color.white;

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
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material = hitMaterial;

            }
            anim.enabled = false;
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material = defaultMaterial[i];

            }
            anim.enabled = true;
        }

        public void GoInvisible()
        {
            IsInvis = true;
            collider.enabled = false;
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material = invisMaterial;

            }
        }

        public void UnInvisible()
        {
            IsInvis = false;
            collider.enabled = true;
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material = defaultMaterial[i];
            }
        }

        // Similar to the damaged method, but gets it's own method for ease of use.
        public virtual void Heal(float heal)
        {
            if (StatsComponent.HealthCurrent + heal > StatsComponent.HealthMax)
                StatsComponent.HealthCurrent = StatsComponent.HealthMax;
            else
                StatsComponent.HealthCurrent += heal;

            //spawn heal effect
            if (healEffect != null)
            {
                GameObject heals = TrashMan.spawn(healEffect, transform.position + healEffectOffset, Quaternion.identity);
                heals.transform.SetParent(transform);
            }
            if (damagePrefab != null)
            {
                //spawn damage number indicator
                Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
                var damageGO = TrashMan.spawn(damagePrefab, screenPoint, Quaternion.identity);
                damageGO.gameObject.SetActive(true);
                damageGO.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
                damageGO.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                damageGO.gameObject.GetComponentInChildren<Text>().text = heal.ToString();
                damageGO.gameObject.GetComponentInChildren<Text>().color = Color.green;


            }
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

        public virtual void OnHit(Entity entity, float damage)
        {
            
        }

        public virtual void UseSkill(Skill skill)
        {

        }

        public virtual void UpdateSkill(Skill skill)
        {
            
        }

        public virtual void PickUpItem(Item item)
        {
            item.transform.parent = InventoryComponent.transform;
        }

        public virtual void OnCast()
        {
            
        }

        public virtual void OnEnable()
        {
            
        }
    }
}