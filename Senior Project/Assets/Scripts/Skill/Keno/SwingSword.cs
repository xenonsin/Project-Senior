using System.Collections;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Seniors.Skills
{
    public class SwingSword : Skill
    {
        [Header("Skill")]
        public bool isActive = false;
        public int damage;
        public bool dashing;
        public float dashSpeed;
        public override void ActivateDown()
        {
            anim.SetTrigger("Attack");
        }

        public override void FixedUpdate()
        {
            if (dashing)
                Dash();
        }

        private void Dash()
        {
            rb.AddForce(rb.position + hc.LastMoveDirection * dashSpeed);
        }

        private void LookAtDirection()
        {
            if (hc.MoveDirection != Vector3.zero)
            {
                hc.LastMoveDirection = hc.MoveDirection;
                hc.transform.rotation = Quaternion.LookRotation(hc.MoveDirection);
            }
        }

        public override void RaiseEvent(string eventName)
        {
            Debug.Log(eventName + " raised");
            switch (eventName)
            {
                case "Attack_EnableTransition":
                    anim.SetBool("EnableTransition", true);
                    break;
                case "Attack_DisableTransition":
                    anim.SetBool("EnableTransition", false);
                    anim.SetBool("CanMove", false);
                    hc.CanMove = false;
                    LookAtDirection();
                    break;
                case "Attack_EnableMove":
                    hc.CanMove = true;
                    anim.SetBool("CanMove", true);

                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit(other);
        }

        public override void OnHit(Collider hit)
        {
            Entitiy entitiy = hit.gameObject.GetComponent<Entitiy>();
            if (entitiy != null)
            {
                if ((hero.enemyFactions & entitiy.currentFaction) == entitiy.currentFaction)
                    entitiy.Damage(damage);
            }
        }
    }
}