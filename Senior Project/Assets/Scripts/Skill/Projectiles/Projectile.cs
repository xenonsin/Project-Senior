using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Hero;
using UnityEngine;

namespace Seniors.Skills.Projectiles
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (BoxCollider))]

    public abstract class Projectile : MonoBehaviour
    {
        public Hero owner;
        private Rigidbody rb;
        private BoxCollider bc;
        public int damage;
        public float speed;
        public float lifeSpan;
        public bool piercing = false;

        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            bc = GetComponent<BoxCollider>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = transform.TransformDirection(Vector3.forward * speed);
            bc.isTrigger = piercing;

            Destroy(gameObject, lifeSpan);
        }

        // when the projectile can collide with another object
        public virtual void OnCollisionEnter(Collision collision)
        {
            Entitiy entity = collision.gameObject.GetComponent<Entitiy>();
            if (entity != null)
            {
                if ((owner.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    entity.Damage(damage);
                    owner.OnHit(entity, damage);
                    Destroy(gameObject);
                }
            }
        }

        // when the projectile can pierce
        public virtual void OnTriggerEnter(Collider collision)
        {
            Entitiy entity = collision.gameObject.GetComponent<Entitiy>();
            if (entity != null)
            {
                if ((owner.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    entity.Damage(damage);
                    owner.OnHit(entity, damage);
                }
            }
        }
    }
}