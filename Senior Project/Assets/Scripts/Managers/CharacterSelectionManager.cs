using Senior.Globals;
using UnityEngine;

namespace Senior.Managers
{
    public class CharacterSelectionManager : MonoBehaviour, ICharacterSelectionManager
    {
        public bool PlayerOneSpawned { get; private set; }
        public bool PlayerTwoSpawned { get; private set; }
        public bool PlayerThreeSpawned { get; private set; }
        public bool PlayerFourSpawned { get; private set; }

        [SerializeField]
        private string playerOneStartButton = "Start_P1";
        [SerializeField]
        private string playerTwoStartButton = "Start_P2";
        [SerializeField]
        private string playerThreeStartButton = "Start_P3";
        [SerializeField]
        private string playerFourStartButton = "Start_P4";

        void Update()
        {
            if (!PlayerOneSpawned)
            {
                if (Input.GetButtonDown(playerOneStartButton))
                {

                }
            }

            if (!PlayerTwoSpawned)
            {
                if (Input.GetButtonDown(playerTwoStartButton))
                {
                    
                }
            }

            if (!PlayerThreeSpawned)
            {
                if (Input.GetButtonDown(playerThreeStartButton))
                {

                }
            }

            if (!PlayerFourSpawned)
            {
                if (Input.GetButtonDown(playerFourStartButton))
                {

                }
            }
        }

        void HandleSpawningInGame()
        {
            
        }

        void GoToCharacterSelectScreen()
        {
            
        }
    }
}