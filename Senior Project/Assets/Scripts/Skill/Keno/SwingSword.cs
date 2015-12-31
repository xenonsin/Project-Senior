using System.Collections;
using UnityEngine;

namespace Seniors.Skills
{
    public class SwingSword : Skill
    {
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
    }
}