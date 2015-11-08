using System;
using System.Collections.Generic;
using Senior.Globals;
using Senior.Inputs;
using UnityEngine;

namespace Senior.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public static GameManager Instance { get; private set; }
        public GameState CurrentGameState { get; set; }
        public static List<int> PlayersInGame = new List<int>();

        public static int NumberOfPlayersInGame
        {
            get { return PlayersInGame.Count; }
        }


        void OnEnable()
        {
            PlayerController.StartButtonPressed += HandleScreenTransitionDependingOnGameState;
        }

        void OnDisable()
        {
            PlayerController.StartButtonPressed -= HandleScreenTransitionDependingOnGameState;
        }

        private void Awake()
        {
            // First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            // Furthermore we make sure that we don't destroy between scenes (this is optional)
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SetGameState(GameState state)
        {
            CurrentGameState = state;
        }

        //TODO: move some logic to main menu.cs
        void HandleScreenTransitionDependingOnGameState(int playerNumber)
        {
#if UNITY_EDITOR
            Debug.Log(string.Format("Player {0} pressed the Start Button!", playerNumber));
#endif

            if (!PlayerIsInGame(playerNumber))
                PlayersInGame.Add(playerNumber);

            switch (GameManager.Instance.CurrentGameState)
            {
                case GameState.MainMenu:
                    UIManager.Instance.DisplayCharacterSelect();
                    UIManager.Instance.CharacterSelect.ActivatePlayerSelectionSprite(playerNumber);
                    break;
                case GameState.CharacterSelect:
                    UIManager.Instance.CharacterSelect.ActivatePlayerSelectionSprite(playerNumber);
                    break;
                case GameState.Playing:
                    //EnableHeroSelectUIWheelSmall
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }

        public bool PlayerIsInGame(int playerNumber)
        {
            foreach (var player in PlayersInGame)
            {
                if (playerNumber == player)
                    return true;
            }

            return false;
        }

        public static void LoadLevel(string levelName)
        {
            if (levelName == Application.loadedLevelName) return;

            switch (levelName)
            {
                case "debug":
                    Application.LoadLevel("debug");

                    break;
                case "levelSelect":
                    break;
            }
        }
    }
}