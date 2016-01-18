using System.Collections;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Hero;
using Senior.Components;
using Senior.Inputs;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Seniors.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public string SkillName = string.Empty;
        public Sprite SkillIcon = null;
        public int SkillIndex = 0;
        public float CoolDown = 0;
        public float CoolDownTimer = 0;
        public bool buttonHold = false;
        public float buttonHoldTimePressed = 0f;
        //type target, projectile aoe
        public int damage = 0;
        public bool knockback = false;
        public float knockbackForce = 1f;
        [Header("Target")]
        public bool isTarget = false;
        public Vector3 targetLocation;
        [Header("Projectile")]
        public bool isProjectile = false;
        public Projectile projectile;
        public float projectileOffset = 1f;
        [Header("AOE")]
        public bool DrawGizmos;
        public Color GizmoColor;
        public float Range;
        public float Angle;
        public Vector3 offset;
        protected bool IsDisabled = false;
        protected Animator anim;
        protected HeroController hc;
        protected Rigidbody rb;
        protected Stats stats;
        protected Hero hero;
        protected SkillsController sc;
        [Header("Colldrs")]
        public BoxCollider BCollider;

        public SphereCollider SCollider;

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            if (buttonHold)
            {
                buttonHoldTimePressed += Time.deltaTime;
            }

            if (IsDisabled)
            {
                CoolDownTimer -= Time.deltaTime;

                //if (countdownTimer > 1)
                hero.UpdateSkill(this);
                if (CoolDownTimer <= 0)
                {
                    IsDisabled = false;
                    Reset();
                }
            }
        }

        public virtual void FixedUpdate()
        {
        }

        private void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = GizmoColor;
                Gizmos.DrawWireSphere(transform.position + offset, Range);


                Debug.DrawRay(transform.position + offset, transform.forward*Range, GizmoColor);
                Debug.DrawRay(transform.position + offset, (Quaternion.Euler(0, Angle, 0)*transform.forward).normalized*Range,
                    GizmoColor);
                Debug.DrawRay(transform.position + offset, (Quaternion.Euler(0, -Angle, 0)*transform.forward).normalized*Range,
                    GizmoColor);

            }
        }

        public virtual void ActivateAOECollider()
        {
            //TODO: over a given time?
            //if overtime might as well be projectile with collider?
            // create a sphere at target location, default is current position with the radius of range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + offset, Range);

            foreach (var hit in hitColliders)
            {
                if (hit)
                {
                    // if we restrict the sphere to a certain angle.
                    // currently only works if collider originates from current position with no offset
                    if (Angle > 0)
                    {
                        var cone = Mathf.Cos(Angle * Mathf.Deg2Rad);
                        Vector3 dir = (hit.transform.position - transform.position + offset).normalized;

                        if (Vector3.Dot(transform.forward, dir) > cone)
                        {
                            OnHit(hit);
                        }
                    }
                    else
                    {
                        OnHit(hit);
                    }
                    
                }
            }
        }

        public virtual void ShootProjectile()
        {
            if (projectile != null)
            {
                Projectile pro = Instantiate(projectile, hero.transform.position + (projectileOffset * hero.transform.forward) + (0.4f * hero.transform.up),
                    hero.transform.rotation) as Projectile;
                if (pro != null)
                {
                    pro.damage = damage;
                    pro.owner = hero;
                }
            }
        }

        public virtual void OnHit(Collider hit)
        {
            Entity entity = hit.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                if ((hero.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    hero.OnHit(entity, damage);
                    entity.Damage(damage);
                    if (knockback)
                    {
                        Vector3 direction = (entity.transform.position - hero.transform.position).normalized;
                        entity.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockbackForce, ForceMode.Impulse);
                    }
                }
            }
            
        }

        public virtual void OnCast()
        {
            hero.UseSkill(this);
        }

        public virtual void Initialize(SkillsController sc, HeroController hc, Hero hero, Animator anim, Rigidbody rb)
        {
            this.hc = hc;
            this.hero = hero;
            this.anim = anim;
            this.rb = rb;
            this.stats = hero.StatsComponent;
            this.sc = sc;
            this.hero.owner.SetSkillIcon(this);
            Reset();
        }

        public virtual void Reset()
        {
            CoolDownTimer = CoolDown;

        }

        public virtual void ActivateDown()
        {
            IsDisabled = true;
            buttonHold = true;
            buttonHoldTimePressed = 0f;
        }

        public virtual void ActivateUp()
        {
            buttonHold = false;
        }

        public virtual void Deactivate()
        {
        }


        public virtual void RaiseEvent(string eventName)
        {           
        }

        //FreezeFrame
        protected IEnumerator FreezeFrame()
        {
            // if frozen = false, froze = true you know just in case
            anim.enabled = false;
            yield return new WaitForSeconds(0.1f);
            anim.enabled = true;
        }

    }
}