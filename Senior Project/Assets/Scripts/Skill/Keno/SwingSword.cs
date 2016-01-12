using System.Collections;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Seniors.Skills
{
    public class SwingSword : Skill
    {
        [Header("Skill")]
        public float damage;
        private bool dashing;
        public float dashSpeed;
        public override void Activate()
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
            rb.AddForce(rb.position + hc.LastMoveDirection * 70);
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
            switch (eventName)
            {
                case "EnableTransition":
                    anim.SetBool("DisableTransitions", false);
                    dashing = false;
                    break;
                case "DisableTransition":
                    anim.SetBool("DisableTransitions", true);
                    dashing = true;
                    LookAtDirection();
                    break;
                case "StartDash":
                    dashing = true;

                    break;
                case "EndDash":
                    dashing = false;

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
                Debug.Log("hi");
                if ((hero.enemyFactions & entitiy.currentFaction) == entitiy.currentFaction)
                    entitiy.Damage(10);
            }
        }
    }
}