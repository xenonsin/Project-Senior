using Assets.Scripts.Entities.Hero;
using Senior.Inputs;
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
        protected Hero hero;

        public virtual void Awake()
        {
        }

        public virtual void Update()
        {
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

        public virtual void OnHit(Collider hit)
        {
        }

        public virtual void OnCast()
        {
            hero.UseSkill(this);
        }

        public virtual void Initialize(HeroController hc, Hero hero, Animator anim, Rigidbody rb)
        {
            this.hc = hc;
            this.hero = hero;
            this.anim = anim;
            this.rb = rb;

            this.hero.owner.SetSkillIcon(this);
            Reset();
        }

        public virtual void Reset()
        {
            CoolDownTimer = CoolDown;

        }

        public virtual void Activate()
        {
            IsDisabled = true;
        }

        public virtual void Deactivate()
        {
        }


        public virtual void RaiseEvent(string eventName)
        {           
        }
    }
}