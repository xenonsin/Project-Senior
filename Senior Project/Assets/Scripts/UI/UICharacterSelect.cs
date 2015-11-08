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
            CharacterCountdown.CountdownExpiration += FinalizePlayerHeroes;
        }

        private void OnDisable()
        {
            PlayerController.LeftButtonPressed -= MovePlayerSelectLeft;
            PlayerController.RightButtonPressed -= MovePlayerSelectRight;
            PlayerController.ConfirmButtonPressed -= ConfirmSelection;
            PlayerController.CancelButtonPressed -= CancelSelection;
            CharacterCountdown.CountdownExpiration -= FinalizePlayerHeroes;
        }

        private void Initialize()
        {
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
            CharacterPortraits[unComfirmedChar].Selected(player);
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
                CharacterPortraits[prevIndex].Deselected(player);
            CharacterPortraits[newIndex].Selected(player);
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
                CharacterPortraits[prevIndex].Deselected(player);
            CharacterPortraits[newIndex].Selected(player);
            playerSelectionIndex[player] = newIndex;
        }

        public void ConfirmSelection(Player player)
        {
            int index = playerSelectionIndex[player];
            CharacterPortraits[index].Confirmed(player);
        }

        public void CancelSelection(Player player)
        {
            int index = playerSelectionIndex[player];
            CharacterPortraits[index].UnConfirm(player);
            CharacterPortraits[index].Selected(player);
        }

        public int SearchRight(Player player)
        {

            int index = playerSelectionIndex[player];

            if (index == CharacterPortraits.Length - 1) return index;

            for (int i = index + 1; i < CharacterPortraits.Length; i++)
            {
                if (!CharacterPortraits[i].IsConfirmed)
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
                if (!CharacterPortraits[i].IsConfirmed)
                {
                    return i;
                }
            }

            return index;
        }

        private int GetUnConfirmedCharacter()
        {

            for (int i = 0; i < CharacterPortraits.Length; i++)
            {
                if (!CharacterPortraits[i].IsConfirmed)
                {
                    return i;
                }
            }
            return 0;

        }

        private void FinalizePlayerHeroes()
        {
            if (GameManager.AllPlayersInGameAreConfirmed()) return;
            foreach (var player in GameManager.PlayersInGame)
            {
                if (player.CurrentState != PlayerState.ConfirmedCharacter)
                {
                    //Give the player a random hero
                    int index = GetUnConfirmedCharacter();
                    if (playerSelectionIndex[player] > index)
                    {
                        MovePlayerSelectRight(player);
                        ConfirmSelection(player);
                    }
                    else if (playerSelectionIndex[player] < index)
                    {
                        MovePlayerSelectLeft(player);
                        ConfirmSelection(player);
                    }
                    else
                    {
                        ConfirmSelection(player);
                    }
                }
            }
        }
    }
}