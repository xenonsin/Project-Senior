using Senior.Globals;
using UnityEngine;

namespace Senior.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentGameState { get; set; }

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

        public void ChangeGameState(GameState state)
        {
            CurrentGameState = state;
        }
    }
}