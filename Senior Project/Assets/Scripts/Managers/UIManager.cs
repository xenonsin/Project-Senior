using UnityEngine;

namespace Senior.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject CharacterSelect;

        public void DisplayMainMenu()
        {
            MainMenu.SetActive(true);
            CharacterSelect.SetActive(false);
        }

        public void DisplayCharacterSelect()
        {
            MainMenu.SetActive(false);
            CharacterSelect.SetActive(true);
        }
    }
}