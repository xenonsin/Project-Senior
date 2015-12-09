using Senior.Globals;
using UnityEngine;

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
        private GameObject hero;

        public GameObject Hero
        {
            get { return hero; }
            set { hero = value; }
        }

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
            if (Hero != null)
            {
                spawnPosition.x += Random.Range(0, 5);
                spawnPosition.z += Random.Range(0, 5);
                GameObject go = Instantiate(hero, spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = this.transform;

                if (HeroSpawned != null)
                    HeroSpawned(this);
            }
        }

        // Spawns the Health/Stamina bars if a hero is spawned, otherwise
        // it spawns a insert credits text.
        public void ShowPlayerUI()
        {
            
        }
    }
}