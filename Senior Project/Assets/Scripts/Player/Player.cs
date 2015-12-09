using System;
using Assets.Scripts.Entities.Hero;
using Senior.Globals;
using Senior.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Senior
{
    public class Player : MonoBehaviour
    {
        public delegate void PlayerAction(Player player);

        public static event PlayerAction HeroSpawned;

        [SerializeField]
        private int playerNumber;

        public int PlayerNumber
        {
            get {return playerNumber;}
            set { playerNumber = value; }
        }
        [SerializeField]
        private PlayerState currentState = PlayerState.WaitingForCredits;

        public PlayerState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        [SerializeField]
        private GameObject heroGO;

        public GameObject HeroGO
        {
            get { return heroGO; }
            set { heroGO = value; }
        }

        public PlayerUI ui;

        public void OnEnable()
        {
            MapScript.MapCreatedSuccess += SpawnPlayer;
        }

        public void OnDisable()
        {
            MapScript.MapCreatedSuccess -= SpawnPlayer;

        }

        // Spawns a Hero that is controlled by the player.
        public void SpawnPlayer(Vector3 spawnPosition)
        {
            if (HeroGO != null)
            {
                spawnPosition.x += Random.Range(0, 5);
                spawnPosition.z += Random.Range(0, 5);
                GameObject go = Instantiate(heroGO, spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = this.transform;

                if (HeroSpawned != null)
                    HeroSpawned(this);

                ShowPlayerUI(go);
            }
        }

        // Spawns the Health/Stamina bars if a hero is spawned, otherwise
        // it spawns a insert credits text. We pass in the initialized game object 
        // because we can't access getcomponent on non initialized prefabs
        public void ShowPlayerUI(GameObject go)
        {
            if (ui != null)
            {

                Hero hero = go.GetComponent<Hero>();
                hero.owner = this;
                ui.Initialize(hero);
                ui.ShowHeroStats();
            }

        }

        public void OnHealthModified(Hero hero)
        {
            ui.OnHealthModified(hero);
        }
    }
}