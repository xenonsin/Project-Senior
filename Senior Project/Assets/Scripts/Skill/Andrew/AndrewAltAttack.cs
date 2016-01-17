

using Senior.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Seniors.Skills.Andrew
{
    public class AndrewAltAttack : Skill
    {
        public float maxChargeTime;
        public ChannelBarWorldUI channelBarPrefab;
        private GameObject channelBarGO;
        private Image channelFill;
        public override void Start()
        {
            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

            ChannelBarWorldUI channel = Instantiate(channelBarPrefab, screenPoint, Quaternion.identity) as ChannelBarWorldUI;
            channelBarGO = channel.gameObject;
            channelBarGO.SetActive(true);
            channel.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
            channel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            channel.offset = new Vector2(0, -20);
            channel.owner = hero;
            channelFill = channel.ChannelFill;
            channelBarGO.SetActive(false);
        }
        public override void ActivateDown()
        {
            base.ActivateDown();
            anim.SetTrigger("AltAttack");
            anim.SetBool("AltHold", true);

            sc.IsBusy = true;
            hc.OnlyRotate = true;
            hc.RotateBasedOnMovement = true;
            channelFill.fillAmount = 0;

            channelBarGO.SetActive(true);

        }

        public override void Update()
        {
            base.Update();

            if (buttonHold)
            {
                channelFill.fillAmount = buttonHoldTimePressed / maxChargeTime;

            }
            else
            {
            }
        }

        public override void ActivateUp()
        {
            base.ActivateUp();
            anim.SetBool("AltHold", false);
            channelBarGO.SetActive(false);
            channelFill.fillAmount = 0;

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