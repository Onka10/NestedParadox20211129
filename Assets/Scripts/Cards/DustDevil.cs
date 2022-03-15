using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Cards{
    public class DustDevil : MonoBehaviour,ICard
    {
        [SerializeField] MonsterManager _monstermanager;
        public bool CheckTrigger(){
            // if(_monstermanager.MonsterCount  >= 1) return true;
            // else return false;

            return true;
        } 
    }
}
