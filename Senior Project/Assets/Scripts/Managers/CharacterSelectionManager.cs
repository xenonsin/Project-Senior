using System;
using Senior.Globals;
using Senior.Inputs;
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

        void OnEnable()
        {
            PlayerController.StartButtonPressed += HandleScreenTransitionDependingOnGameState;
        }

        void OnDisable()
        {
            PlayerController.StartButtonPressed -= HandleScreenTransitionDependingOnGameState;
        }


        void Update()
        {

        }

        void HandleScreenTransitionDependingOnGameState(int playerNumber)
        {
#if UNITY_EDITOR
            Debug.Log(string.Format("Player {0} pressed the Start Button!", playerNumber));
#endif
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