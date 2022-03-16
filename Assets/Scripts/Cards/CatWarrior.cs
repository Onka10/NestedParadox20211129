using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Cards{
    public class CatWarrior : MonoBehaviour,ICard
    {
        public bool CheckTrigger(){
            return true;
        }
    }
}
