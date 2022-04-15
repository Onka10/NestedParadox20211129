using System;
using Cysharp.Threading.Tasks;
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
            // }

            // GuardSummon().Forget();

            Debug.Log("ガードクインテット！！！");
        }

        // private async UniTask GuardSummon(){
        //     CardPresenter.I.PlayerSummon(1);
        //     Debug.Log("しょうかん1");
        //     await UniTask.WaitUntil(() => MonsterManager.I.CanSummon);
        //     CardPresenter.I.PlayerSummon(1);
        //     Debug.Log("しょうかん2");
            
        // }

        // private async UniTask<bool> Summon()
        // {   
        //     await UniTask.Run(() => CardPresenter.I.PlayerSummon(1));
        //     return true; 
        // }
    }
}

