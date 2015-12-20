using Senior.Globals;
using UnityEngine;

namespace Senior.Managers
{
    public class UIManager : MonoBehaviour
    {
        public  GameObject MainMenu;
        public  UICharacterSelect CharacterSelect;
        public  GameObject PlayerUi;
        public static UIManager Instance { get; private set; }

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

        public void DisplayMainMenu()
        {
            MainMenu.SetActive(true);
            CharacterSelect.gameObject.SetActive(false);
            PlayerUi.SetActive(false);
            GameManager.SetGameState(GameState.MainMenu);
        }

        public void DisplayCharacterSelect()
        {
            MainMenu.SetActive(false);
            CharacterSelect.gameObject.SetActive(true);
            PlayerUi.SetActive(false);
            GameManager.SetGameState(GameState.CharacterSelect);
        }

        public void DisplayLoadingGraphic()
        {
            MainMenu.SetActive(false);
            CharacterSelect.gameObject.SetActive(false);
            PlayerUi.SetActive(false);

            GameManager.SetGameState(GameState.Loading);
        }

        public void DisplayInGameStuff()
        {
            MainMenu.SetActive(false);
            CharacterSelect.gameObject.SetActive(false);
            PlayerUi.SetActive(true);

            GameManager.SetGameState(GameState.InGame);
        }
    }
}