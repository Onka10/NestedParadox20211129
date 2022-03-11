using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Cards
{
    public class LastSword : MonoBehaviour,IMagic
    {//最後の剣
        public bool CheckCondition(){
            //多分無し
            return true;
        }

        public void ExecutionMagic(){
            //フィールドのモンスターの数を入手して攻撃力を上昇
        }
    }
}
