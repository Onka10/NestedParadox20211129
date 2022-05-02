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
            return true;
        }

        public void Execution(){
            _monstermanager.QuintetSummon();
            Debug.Log("ガードクインテット！！！");
        }


    }
}

