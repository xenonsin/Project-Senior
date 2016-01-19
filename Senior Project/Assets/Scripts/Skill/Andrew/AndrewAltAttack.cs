

using Senior.Managers;
using Seniors.Skills.Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace Seniors.Skills.Andrew
{
    public class AndrewAltAttack : Skill
    {
        public float maxChargeTime;
        public GameObject chargePrefab;
        private GameObject chargeInstance;
        public float damageMultiplier = 2;
        public float speedMultiplier = 10;
        public override void Start()
        {
            
        }
        public override void ActivateDown()
        {
            if (sc.IsBusy) return;
            base.ActivateDown();
            anim.SetTrigger("AltAttack");
            anim.SetBool("AltHold", true);

            sc.IsBusy = true;
            hc.OnlyRotate = true;
            hc.RotateBasedOnMovement = true;
            hero.channelFill.fillAmount = 0;

            hero.channelBarGO.SetActive(true);
            chargeInstance = Instantiate(chargePrefab, hero.transform.position, Quaternion.identity) as GameObject;
        }

        public override void Update()
        {
            base.Update();

            if (buttonHold)
            {
                hero.channelFill.fillAmount = buttonHoldTimePressed / maxChargeTime;
            }

        }

        public override void ActivateUp()
        {
            base.ActivateUp();
            anim.SetBool("AltHold", false);
            hero.channelBarGO.SetActive(false);
            hero.channelFill.fillAmount = 0;
            
            if (chargeInstance != null)
                Destroy(chargeInstance);
        }

        public override void RaiseEvent(string eventName)
        {
            switch (eventName)
            {
                case "AltAttack_Shoot":
                    ShootProjectile();
                    break;
                case "AltAttack_NotBusy":
                    sc.IsBusy = false;
                    hc.OnlyRotate = false;
                    hc.RotateBasedOnMovement = false;
                    break;
            }
        }

        public override void ShootProjectile()
        {
            if (projectile != null)
            {
                OnCast();
                Projectile pro = Instantiate(projectile, hero.transform.position + (projectileOffset * hero.transform.forward) + (0.4f * hero.transform.up),
                    hero.transform.rotation) as Projectile;
                if (pro != null)
                {
                    pro.damage = damage * (1 + damageMultiplier * (buttonHoldTimePressed / maxChargeTime));
                    pro.owner = hero;
                    pro.speed *= 1 + speedMultiplier * buttonHoldTimePressed/maxChargeTime;
                }
            }
        }
    }
}