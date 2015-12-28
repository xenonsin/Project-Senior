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
        public bool IsDisabled = false;

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

        public virtual void OnHit()
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


    }
}