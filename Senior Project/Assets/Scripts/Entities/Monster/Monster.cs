using Assets.Scripts.Entities.Components;
using Senior.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Monster
{
    public class Monster : Entity
    {
         //Monster Controller
         //AI which keep track of states
         //UI which shows health bar

        private Image HealthFill;
        public HealthBarWorldUI HealthPrefab;
        private GameObject healthGO;

        public override void Awake()
        {
            base.Awake();
            mc = GetComponent<MonsterController>();
        }

        public override void Start()
        {
            base.Start();
            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

            HealthBarWorldUI health = Instantiate(HealthPrefab, screenPoint, Quaternion.identity) as HealthBarWorldUI;
            healthGO = health.gameObject;
            healthGO.SetActive(true);
            health.GetComponent<RectTransform>().SetParent(UIManager.Instance.WorldUi.transform);
            health.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            health.offset = new Vector2(0,40);
            health.owner = this;
            HealthFill = health.HealthFill;
            healthGO.SetActive(false);
        }

        public override void Damage(Entity dealer, float damage)
        {
            base.Damage(dealer,damage);
            Debug.Log(name + " has been hit for " + damage + " damage!");
            healthGO.gameObject.SetActive(true);
            HealthFill.fillAmount = StatsComponent.HealthCurrent/(float)StatsComponent.HealthMax;
        }

        public override void Heal(float heal)
        {
            base.Heal(heal);

            HealthFill.fillAmount = StatsComponent.HealthCurrent / (float)StatsComponent.HealthMax;


            if (StatsComponent.HealthCurrent == StatsComponent.HealthMax)
                healthGO.gameObject.SetActive(false);
        }

        public override void FullHeal()
        {
            base.FullHeal();
            //healthGO.gameObject.SetActive(false);
        }


    }
}