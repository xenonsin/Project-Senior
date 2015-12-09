using UnityEngine;

namespace Senior.Managers
{
    public class PlayerUI : MonoBehaviour
    {
        public GameObject HeroStats;
        public GameObject CoinText;

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
    }
}