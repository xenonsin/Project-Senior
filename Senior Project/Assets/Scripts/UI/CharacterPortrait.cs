using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class CharacterPortrait : MonoBehaviour
    {
        public List<Player> PlayersCurrentlySelecting = new List<Player>();
        public bool IsConfirmed { get; set; }
        public GameObject heroPrefab;
        private Image image;
        private Player confirmedPlayer;
        private void Awake()
        {
            IsConfirmed = false;
            image = GetComponent<Image>();
            image.color = Color.grey;
        }

        public void Selected(Player player)
        {
            if (!PlayersCurrentlySelecting.Contains(player))
                PlayersCurrentlySelecting.Add(player);
            image.color = Color.white;
        }

        public void Deselected(Player player)
        {
            if (PlayersCurrentlySelecting.Contains(player))
                PlayersCurrentlySelecting.Remove(player);
            image.color = Color.grey;
        }

        public void UnConfirm(Player player)
        {
            if (confirmedPlayer != player) return;
            IsConfirmed = false;
            player.Hero = null;
            confirmedPlayer = null;
        }

        public void Confirmed(Player player)
        {
            if (confirmedPlayer != null) return;

            IsConfirmed = true;
            player.Hero = heroPrefab;
            confirmedPlayer = player;
            image.color = Color.yellow;
        }
    }
}