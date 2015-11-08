using UnityEngine;

namespace Senior.Inputs
{
    public interface IPlayerController
    {
        Vector2 MoveInput { get; }
        bool AttackButton { get; }
        bool AltAttackButton { get; }
        bool SkillOneButton { get; }
        bool SkillTwoButton { get; }
        bool SkillThreeButton { get; }
        bool SkillFourButton { get; }
    }
}