using System.Collections.Generic;
using Seniors.Skills.Buffs;
using UnityEngine;

namespace Assets.Scripts.Entities.Components
{
    public class BuffsManager : MonoBehaviour
    {
        public List<Buff> buffs = new List<Buff>();

        public void AddBuff(Buff buff)
        {
            if (!buff.canStack)
            {
                if (buffs.Contains(buff))
                    return;
            }

            buff.transform.SetParent(this.transform);
            buff.OnTick();
            buffs.Add(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            buffs.Remove(buff);
        }
    }
}