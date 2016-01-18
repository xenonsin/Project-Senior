using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Hero;
using UnityEngine;

namespace Seniors.Skills.Projectiles
{


    public class Projectile : MonoBehaviour
    {
        public Hero owner;
        private Rigidbody rb;
        private BoxCollider bc;
        public int damage;
        public float speed;
        public float lifeSpan;
        public bool piercing = false;
        public bool isMoving = true;
        public bool knockback = false;
        public float knockbackForce = 1f;


        public virtual void Start()
        {
            if (isMoving)
            {
                rb = GetComponent<Rigidbody>();
                bc = GetComponent<BoxCollider>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                rb.velocity = transform.TransformDirection(Vector3.forward*speed);
                bc.isTrigger = piercing;
            }
            Destroy(gameObject, lifeSpan);
        }

        // when the projectile can collide with another object
        public virtual void OnCollisionEnter(Collision collision)
        {
            Entity entity = collision.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                if ((owner.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    OnHit(entity);
                    Destroy(gameObject);
                }
            }
        }

        // when the projectile can pierce
        public virtual void OnTriggerEnter(Collider collision)
        {
            Entity entity = collision.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                if ((owner.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    OnHit(entity);
                }
            }
        }

        public virtual void OnHit(Entity target)
        {
            target.Damage(owner, damage);
            owner.OnHit(target, damage);
            if (knockback)
            {
                Vector3 direction = (target.transform.position - owner.transform.position).normalized;
                target.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}