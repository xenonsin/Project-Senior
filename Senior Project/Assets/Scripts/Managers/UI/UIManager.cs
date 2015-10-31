using Senior.Globals;
using UnityEngine;

namespace Senior.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public UICharacterSelect CharacterSelect;
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
            GameManager.Instance.SetGameState(GameState.MainMenu);
        }

        public void DisplayCharacterSelect()
        {
            MainMenu.SetActive(false);
            CharacterSelect.gameObject.SetActive(true);
            GameManager.Instance.SetGameState(GameState.CharacterSelect);
        }

        public void DisplayLoadingGraphic()
        {
            MainMenu.SetActive(false);
            CharacterSelect.gameObject.SetActive(false);
            //DisplayGraphic
            GameManager.Instance.SetGameState(GameState.Loading);
        }

        private void DisplayInGameStuff()
        {
            
        }
    }
}