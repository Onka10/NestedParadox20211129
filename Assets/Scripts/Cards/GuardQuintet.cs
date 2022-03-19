using System;
using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Cards
{
    public class GuardQuintet : MonoBehaviour,ICard,IMagic
    {//ガードクインテット
        public bool CheckTrigger(){
            return true;
        }

        public void Execution(){
            //ガードくんの数が5になるまで召喚を行う。ガードくんのidは1

            // while(GuardKunManager.I.Count < 6){
                // MonsterManager.I.Summon((CardID)Enum.ToObject(typeof(CardID), 1));
                // MonsterManager.I.Summon((CardID)Enum.ToObject(typeof(CardID), 1));
                // MonsterManager.I.Summon((CardID)Enum.ToObject(typeof(CardID), 1));
                // MonsterManager.I.Summon((CardID)Enum.ToObject(typeof(CardID), 1));
                // MonsterManager.I.Summon((CardID)Enum.ToObject(typeof(CardID), 1));
            // }

            Debug.Log("ガードクインテット！！！");
        }
    }
}

