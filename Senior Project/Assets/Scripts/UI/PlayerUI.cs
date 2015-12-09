using Assets.Scripts.Entities.Hero;
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

                //string health = hero.StatsComponent.HealthMax + "/" + hero.StatsComponent.HealthMax;
                //string health = string.Format("{0}/{1}", hero.StatsComponent.HealthMax, hero.StatsComponent.HealthMax);
                //HealthText.text = health;
            }
        }
    }
}