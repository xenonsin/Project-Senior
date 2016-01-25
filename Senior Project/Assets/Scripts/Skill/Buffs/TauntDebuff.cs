using Senior.Managers;
using UnityEngine;

namespace Seniors.Skills.Buffs
{
    public class TauntDebuff : Buff
    {
        public WorldUI TauntUIVFX;
        private GameObject vfxInstance;
        public override void OnAdd()
        {
            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);

            WorldUI vfx = Instantiate(TauntUIVFX, screenPoint, Quaternion.identity) as WorldUI;
            vfx.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
            vfx.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            vfx.offset = new Vector2(0, 30);
            vfx.owner = target;
            vfxInstance = vfx.gameObject;
        }

        public override void OnDisable()
        {
            TrashMan.despawn(vfxInstance);
            base.OnDisable();
        }
    }
}