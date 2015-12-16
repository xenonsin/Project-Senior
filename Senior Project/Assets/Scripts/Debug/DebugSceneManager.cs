using Senior;
using UnityEngine;

namespace Senior.Debugs
{
    public class DebugSceneManager : MonoBehaviour
    {
        public Player player1;
        public Player player2;
        public Player player3;
        public Player player4;

        public GameObject keno;
        public GameObject andrew;
        public GameObject hau;
        public GameObject lung;

        void Start()
        {
            player1.HeroGO = keno;
            player2.HeroGO = andrew;
            player3.HeroGO = hau;
            player4.HeroGO = lung;

            player1.SpawnPlayer(Vector3.zero);
            player2.SpawnPlayer(Vector3.zero);
            //player3.SpawnPlayer(Vector3.zero);
            //player4.SpawnPlayer(Vector3.zero);

        }
    }
}