using System;
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

        public GameObject[] Heroes;

        public GameObject HeroSelectionUI;


        void Update()
        {
            if (!PlayerOneSpawned)
            {
                if (Input.GetButtonDown(playerOneStartButton))
                {
                    PlayerOneSpawned = true;
                    HandleScreenTransitionDependingOnGameState(1);
                }
            }

            if (!PlayerTwoSpawned)
            {
                if (Input.GetButtonDown(playerTwoStartButton))
                {
                    PlayerTwoSpawned = true;
                    HandleScreenTransitionDependingOnGameState(2);
                }
            }

            if (!PlayerThreeSpawned)
            {
                if (Input.GetButtonDown(playerThreeStartButton))
                {
                    PlayerThreeSpawned = true;
                    HandleScreenTransitionDependingOnGameState(3);
                }
            }

            if (!PlayerFourSpawned)
            {
                if (Input.GetButtonDown(playerFourStartButton))
                {
                    PlayerFourSpawned = true;
                    HandleScreenTransitionDependingOnGameState(4);
                }
            }
        }

        void HandleScreenTransitionDependingOnGameState(int playerNumber)
        {
            switch (GameManager.Instance.CurrentGameState)
            {
                case GameState.MainMenu:
                    //SwitchToCharacterSelectScreen
                    //EnableHeroSelectUIWheel
                    break;
                case GameState.CharacterSelect:
                    //EnableHeroSelectUIWheel
                    break;
                case GameState.Playing:
                    //EnableHeroSelectUIWheelSmall
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
    }
}