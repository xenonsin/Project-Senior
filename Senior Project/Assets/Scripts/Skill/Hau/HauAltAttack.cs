using Assets.Scripts.Entities;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Seniors.Skills.Hau
{
    public class HauAltAttack : Skill
    {
        public GameObject TauntPrefab;
        private GameObject tauntInstance;
        public Buff tauntDebuff;

        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            base.ActivateDown();

            tauntInstance = Instantiate(TauntPrefab, hero.transform.position, Quaternion.identity) as GameObject;
            tauntInstance.transform.SetParent(hero.transform);

        }

        public override void Update()
        {
            base.Update();

            if (buttonHold)
            {
                if (buttonHoldTimePressed > .5f)
                {
                    SCollider.enabled = true;
                }
            }

        }

        public override void ActivateUp()
        {
            SCollider.enabled = false;

            base.ActivateUp();

            if (tauntInstance != null)
                Destroy(tauntInstance);
        }

        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity != null)
            {
                if ((hero.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    Buff buff = Instantiate(tauntDebuff, entity.transform.position, Quaternion.identity) as Buff;
                    buff.Initialize(hero, entity);
                }
            }
        }

    }
}