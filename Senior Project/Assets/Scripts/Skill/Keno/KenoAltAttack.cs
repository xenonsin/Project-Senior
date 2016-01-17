using UnityEngine;

namespace Seniors.Skills
{
    public class KenoAltAttack : Skill
    {
        public override void ActivateDown()
        {
            anim.SetTrigger("AltAttack");
        }

        public void ActivateCollider()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit(other);
        }

        public override void OnHit(Collider hit)
        {
            Debug.Log(hit.gameObject.name);
        }
    }
}