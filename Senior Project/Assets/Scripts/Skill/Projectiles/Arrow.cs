using Senior.Items;
using UnityEngine;

namespace Seniors.Skills.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Arrow : Projectile
    {
        public GameObject FireTrail;
        public GameObject IceTrail;
        public GameObject LightningTrail;
        public GameObject DemonicTrail;

        private bool FireActive = false;
        private bool IceActive = false;
        private bool LightningActive = false;
        private bool DemonicActive = false;
        
        public override void Start()
        {
            base.Start();
            FireTrail.SetActive(false);
            IceTrail.SetActive(false);
            LightningTrail.SetActive(false);
            DemonicTrail.SetActive(false);
            for (int i = 0; i < owner.InventoryComponent.items.Count; i++)
            {
                if (owner.InventoryComponent.items[i] is FireEssence)
                {
                    FireTrail.SetActive(true);
                    FireActive = true;
                }
                else if (owner.InventoryComponent.items[i] is IceEssence)
                {
                    IceTrail.SetActive(true);
                    IceActive = true;
                }
                else if (owner.InventoryComponent.items[i] is LightningEssence)
                {
                    LightningTrail.SetActive(true);
                    LightningActive = true;
                }
                else if (owner.InventoryComponent.items[i] is DemonicEssence)
                {
                    DemonicTrail.SetActive(true);
                    DemonicActive = true;
                }
            }
        }
    }
}