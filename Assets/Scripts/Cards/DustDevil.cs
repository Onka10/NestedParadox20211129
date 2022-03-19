using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Cards{
    public class DustDevil :MonoBehaviour, ICard
    {
        public bool CheckTrigger(){
            if(MonsterManager.I.MonsterCount  >= 1) return true;
            else return false;
        } 
    }
}
