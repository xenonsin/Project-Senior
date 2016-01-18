

using Senior.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Seniors.Skills.Andrew
{
    public class AndrewAltAttack : Skill
    {
        public float maxChargeTime;

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
    }
}