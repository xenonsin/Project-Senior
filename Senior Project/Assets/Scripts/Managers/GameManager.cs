using System;
using System.Collections.Generic;
using Senior.Globals;
using Senior.Inputs;
using UnityEngine;

namespace Senior.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static GameState CurrentGameState { get; private set; }
        public static List<Player> PlayersInGame = new List<Player>();

        public static int NumberOfPlayersInGame
        {
            get { return PlayersInGame.Count; }
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

        public static void SetGameState(GameState state)
        {
            CurrentGameState = state;
        }

        public static void AddPlayerToGame(Player player)
        {
            if (!PlayerIsInGame(player))
                PlayersInGame.Add(player);
        }

        public static void RemovePlayerFromGame(Player player)
        {
            if (PlayerIsInGame(player))
                PlayersInGame.Remove(player);
        }

        public static bool PlayerIsInGame(Player player)
        {
            foreach (var p in PlayersInGame)
            {
                if (p == player)
                    return true;
            }

            return false;
        }

        public static bool AllPlayersInGameAreConfirmed()
        {
            foreach (var p in PlayersInGame)
            {
                if (p.CurrentState != PlayerState.ConfirmedCharacter)
                    return false;
            }
            return true;
        }       
        //TODO: Create loading screen
        //TODO: This is a trash method
        public static void LoadLevel(string levelName)
        {
            if (levelName == Application.loadedLevelName) return;

            switch (levelName)
            {
                case "map":
                    Application.LoadLevel("map");

                    break;
                case "levelSelect":
                    break;
            }
        }

        
    }
}