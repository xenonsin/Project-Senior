using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class CharacterPortrait : MonoBehaviour
    {
        public List<Player> PlayersCurrentlySelecting = new List<Player>();
        public bool IsConfirmed { get; set; }
        public Player ConfirmedPlayer { get; set; }

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