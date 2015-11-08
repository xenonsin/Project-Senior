using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Senior.Globals;
using Senior.Inputs;

namespace Senior.Managers
{
    public class UICharacterSelect : MonoBehaviour
    {
        //TODO: Instantiate stuff during awake so that you wont get null errors
        public RectTransform[] PlayerSelectionSprites;
        public CharacterPortrait[] CharacterPortraits;
        //TODO: Make this so that when icons overlap they do not hide each other.
        private Vector2[] PlayerSelctionPositions =
        {
            new Vector2(-247, 215),
            new Vector2(-78, 215),
            new Vector2(83, 215),
            new Vector2(241, 215)
        };

        private Dictionary<Player,int> playerSelectionIndex = new Dictionary<Player, int>(); //player..CurrentSelection

        private int[] PlayerConfirmFlag;

        private void Awake()
        {
            Initialize();
        }


        private void OnEnable()
        {
            PlayerController.LeftButtonPressed += MovePlayerSelectLeft;
            PlayerController.RightButtonPressed += MovePlayerSelectRight;
            PlayerController.ConfirmButtonPressed += ConfirmSelection;
            PlayerController.CancelButtonPressed += CancelSelection;
        }

        private void OnDisable()
        {
            PlayerController.LeftButtonPressed -= MovePlayerSelectLeft;
            PlayerController.RightButtonPressed -= MovePlayerSelectRight;
            PlayerController.ConfirmButtonPressed -= ConfirmSelection;
            PlayerController.CancelButtonPressed -= CancelSelection;
        }

        private void Initialize()
        {
            PlayerConfirmFlag = new int[Global.NUMBER_OF_HEROES_AVAILABLE];
            //Set All Sprites Inactive
            foreach (var sprite in PlayerSelectionSprites)
            {
                sprite.gameObject.SetActive(false);
            }
        }

        public void ActivatePlayerSelectionSprite(Player player)
        {
            if (playerSelectionIndex.ContainsKey(player)) return;
            int index = player.PlayerNumber - 1;
            int unComfirmedChar = GetUnConfirmedCharacter();
            playerSelectionIndex.Add(player, unComfirmedChar);
            PlayerSelectionSprites[index].anchoredPosition = PlayerSelctionPositions[unComfirmedChar];
            CharacterPortraits[unComfirmedChar].Selected();
            PlayerSelectionSprites[index].gameObject.SetActive(true);

        }

        //TODO Make this a delegate
        public void MovePlayerSelectRight(Player player)
        {
            int characterSelectIndex = player.PlayerNumber - 1;
            int prevIndex = playerSelectionIndex[player];
            int newIndex = SearchRight(player);
            PlayerSelectionSprites[characterSelectIndex].anchoredPosition = PlayerSelctionPositions[newIndex];

            bool playerSelected = false;
            foreach (var p in playerSelectionIndex)
            {
                if (p.Key != player && p.Value == prevIndex)
                    playerSelected = true;
            }
            if (!playerSelected)
                CharacterPortraits[prevIndex].Deselected();
            CharacterPortraits[newIndex].Selected();
            playerSelectionIndex[player] = newIndex;

        }

        public void MovePlayerSelectLeft(Player player)
        {
            int characterSelectIndex = player.PlayerNumber - 1;
            int prevIndex = playerSelectionIndex[player];
            int newIndex = SearchLeft(player);
            PlayerSelectionSprites[characterSelectIndex].anchoredPosition = PlayerSelctionPositions[newIndex];

            bool playerSelected = false;
            foreach (var p in playerSelectionIndex)
            {
                if (p.Key != player && p.Value == prevIndex)
                    playerSelected = true;
            }
            if (!playerSelected)
                CharacterPortraits[prevIndex].Deselected();
            CharacterPortraits[newIndex].Selected();
            playerSelectionIndex[player] = newIndex;
        }

        public void ConfirmSelection(Player player)
        {
            int index = playerSelectionIndex[player];
            PlayerConfirmFlag[index] = 1;
            CharacterPortraits[index].Confirmed();
        }

        public void CancelSelection(Player player)
        {
            int index = playerSelectionIndex[player];
            PlayerConfirmFlag[index] = 0;
            CharacterPortraits[index].Selected();
        }

        public int SearchRight(Player player)
        {

            int index = playerSelectionIndex[player];

            if (index == PlayerConfirmFlag.Length - 1) return index;

            for (int i = index + 1; i < PlayerConfirmFlag.Length; i++)
            {
                if (PlayerConfirmFlag[i] == 0)
                {
                    return i;
                }
            }

            return index;
        }

        public int SearchLeft(Player player)
        {
            int index = playerSelectionIndex[player];

            if (index == 0) return index;

            for (int i = index - 1; i >= 0; i--)
            {
                if (PlayerConfirmFlag[i] == 0)
                {
                    return i;
                }
            }

            return index;
        }

        private int GetUnConfirmedCharacter()
        {

            for (int i = 0; i < PlayerConfirmFlag.Length; i++)
            {
                if (PlayerConfirmFlag[i] == 0)
                {
                    return i;
                }
            }
            return 0;

        }
    }
}