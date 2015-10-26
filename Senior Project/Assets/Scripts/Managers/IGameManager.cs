using Senior.Globals;

namespace Senior.Managers
{
    public interface IGameManager
    {
        GameState CurrentGameState { get; set; }
    }
}