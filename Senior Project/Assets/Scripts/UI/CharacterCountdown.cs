using Senior.Globals;
using Senior.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class CharacterCountdown : MonoBehaviour
    {
        public delegate void CountdownAction();

        public static event CountdownAction CountdownExpiration;

        private Text countdownText;
        public float defaultCountdownTimer = 30f;

        public bool IsEnabled = false;

        void Awake()
        {
            countdownText = GetComponent<Text>();
        }


        void OnEnable()
        {
            PlayerController.ConfirmButtonPressed += DecrementTimer;
            IsEnabled = true;
        }

        void OnDisable()
        {
            PlayerController.ConfirmButtonPressed -= DecrementTimer;
            IsEnabled = false;
            Reset();
        }

        void Reset()
        {
            defaultCountdownTimer = 30f;
        }

        void DecrementTimer(Player player)
        {
            if (GameManager.AllPlayersInGameAreConfirmed())
                defaultCountdownTimer -= 1;
        }

        void Update()
        {
            if (IsEnabled)
            {
                defaultCountdownTimer -= Time.deltaTime;
                countdownText.text = Mathf.Floor(defaultCountdownTimer).ToString();

                if (defaultCountdownTimer < 0)
                {
                    if (CountdownExpiration != null)
                        CountdownExpiration();

                    //have callback or delay so that players can see what heroes they random.

                    UIManager.Instance.DisplayInGameStuff();
                    GameManager.LoadLevel("debug");
                    //Loading Screen
                    IsEnabled = false;
                    Reset();
                }
            }
        }
    }
}