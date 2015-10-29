using System;
using Senior.Globals;
using Senior.Inputs;
using UnityEngine;

namespace Senior.Managers
{
    public class CharacterSelectionManager : MonoBehaviour, ICharacterSelectionManager
    {
        //public bool PlayerOneSpawned { get; private set; }
        //public bool PlayerTwoSpawned { get; private set; }
        //public bool PlayerThreeSpawned { get; private set; }
        //public bool PlayerFourSpawned { get; private set; }

        public UIManager uiManager;

        public GameObject[] Heroes;

        //public GameObject HeroSelectionUI;

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
                    uiManager.DisplayCharacterSelect();
                    uiManager.CharacterSelect.ActivatePlayerSelectionSprite(playerNumber);
                    break;
                case GameState.CharacterSelect:
                    uiManager.CharacterSelect.ActivatePlayerSelectionSprite(playerNumber);
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