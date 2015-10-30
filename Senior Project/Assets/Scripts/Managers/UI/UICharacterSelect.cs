using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Senior.Globals;

namespace Senior.Managers
{
    public class UICharacterSelect : MonoBehaviour
    {
        //TODO: Instantiate stuff during awake so that you wont get null errors
        public RectTransform[] PlayerSelectionSprites;
        public CharacterPortrait[] CharacterPortraits;
        private Vector2[] PlayerSelctionPositions =
        {
            new Vector2(-247, 215),
            new Vector2(-78, 215),
            new Vector2(83, 215),
            new Vector2(241, 215)
        };

        private Dictionary<int,int> playerSelectionIndex = new Dictionary<int, int>(); //playerNum..CurrentSelection

        private int[] PlayerConfirmFlag;

        private void Awake()
        {
            Initialize();
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

        public void ActivatePlayerSelectionSprite(int playerNum)
        {
            if (playerSelectionIndex.ContainsKey(playerNum)) return;

            int index = --playerNum;
            int unComfirmedChar = GetUnConfirmedCharacter();
            playerSelectionIndex.Add(playerNum, unComfirmedChar);
            PlayerSelectionSprites[index].anchoredPosition = PlayerSelctionPositions[unComfirmedChar];
            CharacterPortraits[unComfirmedChar].Selected();
            PlayerSelectionSprites[index].gameObject.SetActive(true);

        }

        public void ConfirmSelection(int playerNum)
        {
            int index = playerSelectionIndex[playerNum];
            PlayerConfirmFlag[index] = 1;
        }

        public int SearchRight(int playerNum)
        {
            int index = playerSelectionIndex[playerNum];

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
            int index = playerSelectionIndex[playerNum];

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