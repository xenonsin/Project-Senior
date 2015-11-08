using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class CharacterCountdown : MonoBehaviour
    {
        private Text countdownText;
        public float defaultCountdownTimer = 30f;

        public bool IsEnabled = false;

        void Awake()
        {
            countdownText = GetComponent<Text>();
        }

        void OnEnable()
        {
            IsEnabled = true;
        }

        void OnDisable()
        {
            IsEnabled = false;
            Reset();
        }

        void Reset()
        {
            defaultCountdownTimer = 30f;
        }

        void Update()
        {
            if (IsEnabled)
            {
                defaultCountdownTimer -= Time.deltaTime;
                countdownText.text = Mathf.Floor(defaultCountdownTimer).ToString();

                if (defaultCountdownTimer < 0)
                {
                    GameManager.LoadLevel("debug");
                    //GameManager.Instance.s
                }
            }
        }
    }
}