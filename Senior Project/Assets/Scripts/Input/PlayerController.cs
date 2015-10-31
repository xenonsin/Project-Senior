using System;
using UnityEngine;
using System.Collections;
using Senior.Globals;

namespace Senior.Inputs
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        public delegate void PlayerAction(int number);

        public static event PlayerAction StartButtonPressed;
        public static event PlayerAction LeftButtonPressed;
        public static event PlayerAction RightButtonPressed;

        [SerializeField]
        private int playerNum;

        private PlayerState currentState = PlayerState.WaitingForCredits;

        private string horizontalControlName;
        private string verticalControlName;
        private string attackButtonName;
        private string altAttackButtonName;
        private string skillOneButtonName;
        private string skillTwoButtonName;
        private string skillThreeButtonName;
        private string skillFourButtonName;
        private string startButtonName;

        public Vector2 MoveInput { get; private set; }
        public bool AttackButton { get; private set; }
        public bool AltAttackButton { get; private set; }
        public bool SkillOneButton { get; private set; }
        public bool SkillTwoButton { get; private set; }
        public bool SkillThreeButton { get; private set; }
        public bool SkillFourButton { get; private set; }
        public bool StartButton { get; private set; }

        private bool isAxisInUse = false;

        private void Awake()
        {
            InitializePlayerControls(playerNum);
        }

        public void InitializePlayerControls(int playerNumber)
        {
            horizontalControlName = "Horizontal_P" + playerNumber;
            verticalControlName = "Vertical_P" + playerNumber;
            attackButtonName = "Attack_P" + playerNumber;
            altAttackButtonName = "AltAttack_P" + playerNumber;
            skillOneButtonName = "SkillOne_P" + playerNumber;
            skillTwoButtonName = "SkillTwo_P" + playerNumber;
            skillThreeButtonName = "SkillThree_P" + playerNumber;
            skillFourButtonName = "SkillFour_P" + playerNumber;
            startButtonName = "Start_P" + playerNumber;

#if UNITY_EDITOR
            Debug.Log(string.Format("Player {0} initialized.", playerNumber));
#endif

        }

        private void Update()
        {
            MoveInput = new Vector2(Input.GetAxisRaw(horizontalControlName), Input.GetAxisRaw(verticalControlName));
            AttackButton = Input.GetButtonDown(attackButtonName);
            AltAttackButton = Input.GetButtonDown(altAttackButtonName);
            SkillOneButton = Input.GetButtonDown(skillOneButtonName);
            SkillTwoButton = Input.GetButtonDown(skillTwoButtonName);
            SkillThreeButton = Input.GetButtonDown(skillThreeButtonName);
            SkillFourButton = Input.GetButtonDown(skillFourButtonName);
            StartButton = Input.GetButtonDown(startButtonName);

            if (StartButton)
            {
                if (StartButtonPressed != null)
                    StartButtonPressed(playerNum);

                //debug
                currentState = PlayerState.ChoosingCharacter;
            }

            if (currentState == PlayerState.ChoosingCharacter)
            {
                if (MoveInput.x < 0)
                {
                    if (!isAxisInUse)
                    {
                        isAxisInUse = true;
                        if (LeftButtonPressed != null)
                            LeftButtonPressed(playerNum);
                    }

                }
                else if (MoveInput.x > 0)
                {
                    if (!isAxisInUse)
                    {
                        isAxisInUse = true;
                        if (RightButtonPressed != null)
                            RightButtonPressed(playerNum);
                    }
                }
                else if (MoveInput.x == 0)
                {
                    isAxisInUse = false;
                }
            }

        }
    }
}