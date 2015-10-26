namespace Senior.Managers
{
    public interface ICharacterSelectionManager
    {
        bool PlayerOneSpawned { get; }
        bool PlayerTwoSpawned { get; }
        bool PlayerThreeSpawned { get; }
        bool PlayerFourSpawned { get; }
    }
}