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
        private float nextActionTime = 0f;

        public virtual void Initialize(Entity origin, Entity target)
        {
            this.target = target;
            this.owner = origin;

            transform.SetParent(target.transform);
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
            
        }
    }
}