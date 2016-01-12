using UnityEngine;

namespace Senior.Managers
{
    public class SetLifeSpawn : MonoBehaviour
    {
        public Object myGameObjectOrComponent;
        public float timer;

        void Start()
        {
            if (myGameObjectOrComponent == null)
                myGameObjectOrComponent = gameObject;

            Destroy(myGameObjectOrComponent, timer);
        }
    }
}