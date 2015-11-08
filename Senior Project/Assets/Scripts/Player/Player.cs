using Senior.Globals;
using UnityEngine;

namespace Senior
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int playerNumber;

        public int PlayerNumber
        {
            get {return playerNumber;}
            set { playerNumber = value; }
        }
        [SerializeField]
        private PlayerState currentState = PlayerState.WaitingForCredits;

        public PlayerState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        [SerializeField]
        private GameObject hero;

        public GameObject Hero
        {
            get { return hero; }
            set { hero = value; }
        }
    }
}