using Assets.Scripts.Entities.Hero;
using Senior.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class PlayerUI : MonoBehaviour
    {
        public GameObject HeroStats;
        public GameObject CoinText;

        public Image HeroPorait;

        public Text HealthText;
        public Image HealthFill;

        public void Awake()
        {
            ShowCoinText();
        }

        public void ShowHeroStats()
        {
            CoinText.SetActive(false);
            HeroStats.SetActive(true);
        }

        public void ShowCoinText()
        {
            CoinText.SetActive(true);
            HeroStats.SetActive(false);
        }

        public void Initialize(Hero hero)
        {
            if (hero != null)
            {
                if (hero.Portrait != null)
                    HeroPorait.sprite = hero.Portrait;
            }
        }

        public void OnHealthModified(Hero hero)
        {
            SetHealthText(hero.StatsComponent.HealthCurrent, hero.StatsComponent.HealthMax);
        }

        public void SetHealthText(int current, int max)
        {
            string health = string.Format("{0}/{1}", current, max);
            HealthText.text = health;
            HealthFill.fillAmount = current/(float)max;
        }

        //Show item icon in player ui
        public void OnItemPickUp(Item item)
        {
        }

        // When a player dies, show the countdown timer to allow them to continue with the same hero
        public void OnDead(Hero hero)
        {
        }
    }
}