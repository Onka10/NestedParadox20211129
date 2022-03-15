using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Cards
{
    public class GuardQuintet : MonoBehaviour,ICard,IMagic
    {//ガードクインテット
        public bool CheckTrigger(){
            //無し？ガードくんが1体以上要る時？
            return true;
        }

        public void Execution(){
            //ガードくんの数が5になるまで召喚を行う。ガードくんのidは1
            //召喚の関数が出来てから

            // while(ガードくんのゲッタ < 6){
            //     monsterManager.Summon(1);
            // }

            Debug.Log("ガードクインテット！！！");
        }
    }
}

