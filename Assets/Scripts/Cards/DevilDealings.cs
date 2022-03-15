using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;
using NestedParadox.Players;

namespace NestedParadox.Cards
{
    public class DevilDealings : MonoBehaviour,ICard,IMagic
    {//悪魔の取引

        [SerializeField] MonsterManager monsterManager;
        [SerializeField] PlayerCore playerCore;

        //定数ダメージ
        private int damagecost = 20;

        void Start(){
        }

        public bool CheckTrigger(){
            //HPがダメージより多いなら発動可能
            if(playerCore.Hp.Value > damagecost){
                return true;
            }else{
                return false;
            }
        }

        public void Execution(){
            //HPを減らす
            playerCore.DirectDamaged(damagecost);

            //ダストデビルを召喚。ダストデビルのカードidは0
            // monsterManager.Summon(0);

            Debug.Log("悪魔の取引！！！");
        }
    }
}
