using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Cards
{
    public class GuardQuintet : MonoBehaviour,IMagic
    {//ガードクインテット
        public bool CheckCondition(){
            //無し？ガードくんが1体以上要る時？
            return true;
        }

        public void ExecutionMagic(){
            //ガードくんの数が5になるまで召喚を行う。ガードくんのidは1
            //召喚の関数が出来てから

            // while(ガードくんのゲッタ < 6){
            //     monsterManager.Summon(1);
            // }
        }
    }
}

