using Senior.Globals;
using UnityEngine;

namespace Senior.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public GameState CurrentGameState { get; set; }

        public void ChangeGameState(GameState state)
        {
            CurrentGameState = state;
        }
    }
}