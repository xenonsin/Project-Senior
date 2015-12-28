namespace Senior.Globals
{
    public enum GameState
    {
        MainMenu,
        CharacterSelect,
        InGame,
        Loading,
    }

    public enum PlayerState
    {
        ChoosingCharacter,
        ConfirmedCharacter,
        Playing,
        WaitingForCredits,
        HeroDead,
    }

    public enum SkillTargeting
    {
        Self,
        Auto
    }

}