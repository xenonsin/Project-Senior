using Assets.Scripts.Entities;
using Senior.Globals;
using UnityEngine;

namespace Seniors.Skills.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public BuffType type;
        public Entity target;
        public Entity owner;
        public float lifeSpan = 1f;
        public float period = 1f;
        public bool canStack = false;
        //TODO: make a buff manager
        private float nextActionTime = 0f;

        public virtual void Initialize(Entity origin, Entity target)
        {
            this.target = target;
            owner = origin;
            owner.BuffManager.AddBuff(this);
            OnAdd();
        }

        public virtual void OnAdd()
        {
            
        }

        public virtual void Start()
        {
            Destroy(gameObject, lifeSpan);
        }

        public virtual void Update()
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;
                OnTick();
            }
        }

        public virtual void OnTick()
        {
            owner.BuffManager.RemoveBuff(this);
        }
    }
}