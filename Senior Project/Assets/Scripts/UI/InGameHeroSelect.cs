using System.Collections.Generic;
using Assets.Scripts.Entities.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class InGameHeroSelect : MonoBehaviour
    {
        public Image HeroPortait;
        public GameObject ArrowRight;
        public GameObject ArrowLeft;

        public int selectionIndex = 0;

        void OnEnable()
        {
            GameManager.InactiveHeroListModified += Refresh;
        }

        void OnDisable()
        {
            GameManager.InactiveHeroListModified -= Refresh;
        }

        public void Start()
        {
            Refresh();
            DisplayArrow();
        }

        public void Refresh()
        {
            if (GameManager.HeroesNotInGame.Count > 0)
            {
                Hero hero = GameManager.HeroesNotInGame[0].GetComponent<Hero>();

                if (hero != null)
                    HeroPortait.sprite = hero.Portrait;
                selectionIndex = 0;

                DisplayArrow();

            }
        }

        public void OnSelectLeft(Player player)
        {
            selectionIndex--;
            if (selectionIndex < 0)
                selectionIndex = 0;
            DisplayArrow();

            Hero hero = GameManager.HeroesNotInGame[selectionIndex].GetComponent<Hero>();

            if (hero != null)
                HeroPortait.sprite = hero.Portrait;
        }

        public void OnSelectRight(Player player)
        {
            int lastHeroIndex = GameManager.HeroesNotInGame.Count - 1;
            selectionIndex++;
            if (selectionIndex > lastHeroIndex)
                selectionIndex = lastHeroIndex;

            DisplayArrow();


            Hero hero = GameManager.HeroesNotInGame[selectionIndex].GetComponent<Hero>();

            if (hero != null)
                HeroPortait.sprite = hero.Portrait;
        }

        public void OnConfirm(Player player)
        {
            GameManager.GrantPlayerHero(player,GameManager.HeroesNotInGame[selectionIndex]);
            // find a place to spawn..
            player.SpawnPlayer(Vector3.zero);
        }

        public void DisplayArrow()
        {
            int lastHeroIndex = GameManager.HeroesNotInGame.Count - 1;

            ArrowLeft.SetActive(true);
            ArrowRight.SetActive(true);

            if (selectionIndex == 0)
                ArrowLeft.SetActive(false);
            if (selectionIndex == lastHeroIndex)
                ArrowRight.SetActive(false);
            
        }
    
    }
}