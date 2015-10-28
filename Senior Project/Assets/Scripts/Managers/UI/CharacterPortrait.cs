using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class CharacterPortrait : MonoBehaviour
    {
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
            Deselected();
        }

        public void Selected()
        {
            image.color = Color.white;
        }

        public void Deselected()
        {
            image.color = Color.grey;
        }

        public void Confirmed()
        {
            image.color = Color.yellow;
        }
    }
}