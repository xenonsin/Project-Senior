using System;
using UnityEngine;
using System.Collections;
using Senior.Globals;

namespace Senior.Inputs
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private Player player;

        public delegate void PlayerAction(Player player);

        public static event PlayerAction StartButtonPressed;
        public static event PlayerAction LeftButtonPressed;
        public static event PlayerAction RightButtonPressed;
        public static event PlayerAction ConfirmButtonPressed;
        public static event PlayerAction CancelButtonPressed;

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
            player = GetComponent<Player>();

            InitializePlayerControls(player.PlayerNumber);
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
                    StartButtonPressed(player);

                //debug hack
                if (player.CurrentState != PlayerState.ConfirmedCharacter)
                    player.CurrentState = PlayerState.ChoosingCharacter;
            }

            if (player.CurrentState == PlayerState.ChoosingCharacter)
            {
                if (MoveInput.x < 0)
                {
                    if (!isAxisInUse)
                    {
                        isAxisInUse = true;
                        if (LeftButtonPressed != null)
                            LeftButtonPressed(player);
                    }

                }
                else if (MoveInput.x > 0)
                {
                    if (!isAxisInUse)
                    {
                        isAxisInUse = true;
                        if (RightButtonPressed != null)
                            RightButtonPressed(player);
                    }
                }
                else if (MoveInput.x == 0)
                {
                    isAxisInUse = false;
                }

                if (AttackButton)
                {
                    if (ConfirmButtonPressed != null)
                        ConfirmButtonPressed(player);
                    //BUG If the currently highlighted one is confirmed.. then it locks the player..
                    //NEED CALLBACKS BRO
                    player.CurrentState = PlayerState.ConfirmedCharacter;
                }
            }

            if (player.CurrentState == PlayerState.ConfirmedCharacter)
            {
                if (AttackButton)
                {
                    if (ConfirmButtonPressed != null)
                        ConfirmButtonPressed(player);
                }
                if (AltAttackButton)
                {
                    if (CancelButtonPressed != null)
                        CancelButtonPressed(player);
                    player.CurrentState = PlayerState.ChoosingCharacter;
                }
            }

        }
    }
}