using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using NestedParadox.Monsters;

namespace NestedParadox.Cards
{
    public class GuardQuintet : MonoBehaviour,ICard,IMagic
    {//ガードクインテット
    [SerializeField] MonsterManager _monstermanager;
        public bool CheckTrigger(){
            if(GuardKunManager.I.Count > 0) return true;
            else return false;
        }

        public void Execution(){
            _monstermanager.QuintetSummon();
            Debug.Log("ガードクインテット！！！");
        }

        public string GetText(){
            return "フィールドのガードくんを5体に増やす";
        }


    }
}

