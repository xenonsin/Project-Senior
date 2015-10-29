using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Senior.Managers
{
    public class UICharacterSelect : MonoBehaviour
    {
        //TODO: Instantiate stuff during awake so that you wont get null errors
        public int NumberOfHeroes = 4;
        public RectTransform[] PlayerSelectionSprites;
        private int numberOfPlayersInGame = 0;
        public CharacterPortrait[] CharacterPortraits;
        private Vector2[] PlayerSelctionPositions =
        {
            new Vector2(-247, 215),
            new Vector2(-78, 215),
            new Vector2(83, 215),
            new Vector2(241, 215)
        };

        private Dictionary<int,int> playerIndex = new Dictionary<int, int>(); //playerNum..CurrentSelection

        private int[] PlayerConfirmFlag;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            PlayerConfirmFlag = new int[NumberOfHeroes];
            //Set All Sprites Inactive
            foreach (var sprite in PlayerSelectionSprites)
            {
                sprite.gameObject.SetActive(false);
            }
        }

        public void ActivatePlayerSelectionSprite(int playerNum)
        {
            if (numberOfPlayersInGame < 4)
            {
                numberOfPlayersInGame++;
                int pindex = --playerNum;
                int unComfirmedChar = GetUnConfirmedCharacter();
                playerIndex.Add(playerNum, unComfirmedChar);
                PlayerSelectionSprites[pindex].anchoredPosition = PlayerSelctionPositions[unComfirmedChar];
                CharacterPortraits[unComfirmedChar].Selected();
                PlayerSelectionSprites[pindex].gameObject.SetActive(true);
            }
        }

        public void ConfirmSelection(int playerNum)
        {
            int index = playerIndex[playerNum];
            PlayerConfirmFlag[index] = 1;
        }

        public int SearchRight(int playerNum)
        {
            int index = playerIndex[playerNum];

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

        public int SearchLeft(int playerNum)
        {
            int index = playerIndex[playerNum];

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